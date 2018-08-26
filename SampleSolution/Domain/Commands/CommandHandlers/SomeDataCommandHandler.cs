using System;
using System.Threading;
using System.Threading.Tasks;
using Marten.Services;
using SampleSolution.Common.Domain.Commands;
using SampleSolution.Common.Domain.Events;
using SampleSolution.Domain.Commands.Commands;
using SampleSolution.Domain.Events.Events;
using SampleSolution.Repositories;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using SampleSolution.Data.Contexts;
using SampleSolution.Domain.Aggregates;
using SampleSolution.Mappers;
using SampleSolution.Repositories.UnitOfWork;

namespace SampleSolution.Domain.Commands.CommandHandlers
{
    public class SomeDataCommandHandler : 
        ICommandHandler<CreateSomeDataCommand>, 
        ICommandHandler<DeleteSomeDataCommand>,
        ICommandHandler<UpdateSomeDataCommand>,
        ICommandHandler<ShareContactCommand>
    {
        private readonly IEventBus _eventBus;

        private readonly SomeDataContext _dbContext;
        private readonly ISomeDataWriteRepository _someDataWriteRepository;
        private readonly IBusinessUserRepository _businessUserRepositoy;

        public SomeDataCommandHandler(IEventBus eventBus, SomeDataContext dbContext, ISomeDataWriteRepository someDataWriteRepository, IBusinessUserRepository businessUserRepositoy)
        {
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _someDataWriteRepository = someDataWriteRepository ?? throw new ArgumentNullException(nameof(someDataWriteRepository));
            _businessUserRepositoy = businessUserRepositoy ?? throw new ArgumentNullException(nameof(businessUserRepositoy));
        }

        public Task<Unit> Handle(CreateSomeDataCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using (var context = _dbContext)
                {
                    //Alternatively make aggregate DDD repository on top of existing repositories.
                    var businessUser = _businessUserRepositoy.GetByApplicationUserId(request.ApplicationUserId, context);
                    var someData = SomeAggregate.Create(request, businessUser.Id);
                    _someDataWriteRepository.Create(someData, context);

                    context.SaveChanges();
                }

                _eventBus.Publish(new SomeDataCreatedEvent(request.Id,
                    request.FirstName,
                    request.MiddleName,
                    request.LastName,
                    request.Title,
                    request.CreationDate));

                return Unit.Task;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public Task<Unit> Handle(DeleteSomeDataCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using (var context = _dbContext)
                {
                    var aggregate = _someDataWriteRepository.Get(request.Id, context);
                    _someDataWriteRepository.Delete(aggregate, context);

                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            _eventBus.Publish(new SomeDataDeletedEvent(request.Id));

            return Unit.Task;
        }

        public Task<Unit> Handle(UpdateSomeDataCommand request, CancellationToken cancellationToken)
        {

            try
            {
                using (var context = _dbContext)
                {
                    var aggregate = _someDataWriteRepository.Get(request.SomeDataId, context);
                    aggregate.ChangeFields(request);
                    _someDataWriteRepository.Save(aggregate, context);

                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            _eventBus.Publish(new SomeDataUpdatedEvent(request.SomeDataId,
                request.FirstName,
                request.MiddleName,
                request.LastName,
                request.Title,
                request.Color,
                request.CreationDate,
                request.FacebookUrl));

            return Unit.Task;
        }

        public Task<Unit> Handle(ShareContactCommand request, CancellationToken cancellationToken)
        {
           // _someDataRepository.CopyToNewUser(request);

            return Unit.Task;
        }
    }
}

