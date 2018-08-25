using System;
using System.Threading;
using System.Threading.Tasks;
using SampleSolution.Common.Domain.Commands;
using SampleSolution.Common.Domain.Events;
using SampleSolution.Domain.Commands.Commands;
using SampleSolution.Domain.Events.Events;
using SampleSolution.Repositories;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using SampleSolution.Domain.Aggregates;
using SampleSolution.Mappers;

namespace SampleSolution.Domain.Commands.CommandHandlers
{
    public class SomeDataCommandHandler : 
        ICommandHandler<CreateSomeDataCommand>, 
        ICommandHandler<DeleteSomeDataCommand>,
        ICommandHandler<UpdateSomeDataCommand>,
        ICommandHandler<ShareContactCommand>
    {
        private readonly IEventBus _eventBus;
        private readonly ISomeDataWriteRepository _someDataRepository;
        private readonly IBusinessUserRepositoy _businessUserRepositoy;

        public SomeDataCommandHandler(IEventBus eventBus, ISomeDataWriteRepository someDataRepository, IBusinessUserRepositoy businessUserRepositoy)
        {
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _someDataRepository = someDataRepository ?? throw new ArgumentNullException(nameof(someDataRepository));
            _businessUserRepositoy = businessUserRepositoy ?? throw new ArgumentNullException(nameof(businessUserRepositoy));
        }

        public Task<Unit> Handle(CreateSomeDataCommand request, CancellationToken cancellationToken)
        {
            var someData = SomeAggregate.Create(request);

            _someDataRepository.Create(someData);

            _eventBus.Publish(new SomeDataCreatedEvent(request.Id,
                request.FirstName,
                request.MiddleName,
                request.LastName,
                request.Title,
                request.CreationDate));

            return Unit.Task;
        }

        public Task<Unit> Handle(DeleteSomeDataCommand request, CancellationToken cancellationToken)
        {
            _someDataRepository.Delete(request);

            _eventBus.Publish(new SomeDataDeletedEvent(request.Id));

            return Unit.Task;
        }

        public Task<Unit> Handle(UpdateSomeDataCommand request, CancellationToken cancellationToken)
        {
            _someDataRepository.Update(request);

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
            _someDataRepository.CopyToNewUser(request);

            return Unit.Task;
        }
    }
}

