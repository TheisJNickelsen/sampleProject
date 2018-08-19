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

namespace SampleSolution.Domain.Commands.CommandHandlers
{
    public class SomeDataCommandHandler : 
        ICommandHandler<CreateSomeDataCommand>, 
        ICommandHandler<DeleteSomeDataCommand>,
        ICommandHandler<UpdateSomeDataCommand>
    {
        private readonly IEventBus _eventBus;
        private readonly ISomeDataRepository _someDataRepository;

        public SomeDataCommandHandler(IEventBus eventBus, ISomeDataRepository someDataRepository)
        {
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _someDataRepository = someDataRepository ?? throw new ArgumentNullException(nameof(someDataRepository));
        }

        public Task<Unit> Handle(CreateSomeDataCommand request, CancellationToken cancellationToken)
        {
            _someDataRepository.Create(request);

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
    }
}

