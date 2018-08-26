using SampleSolution.Common.Domain.Commands;
using SampleSolution.Domain.Commands.Commands;
using SampleSolution.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using SampleSolution.Common.Domain.Events;
using SampleSolution.Domain.Events.Events;

namespace SampleSolution.Domain.Commands.CommandHandlers
{
    public class BusinessUserCommandHandler :
        ICommandHandler<CreateBusinessUserCommand>
    {
        private readonly IBusinessUserRepository _businessUserRepositoy;
        private readonly IEventBus _eventBus;

        public BusinessUserCommandHandler(IBusinessUserRepository businessUserRepositoy, IEventBus eventBus)
        {
            _businessUserRepositoy = businessUserRepositoy ?? throw new ArgumentNullException(nameof(businessUserRepositoy));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        public Task<Unit> Handle(CreateBusinessUserCommand request, CancellationToken cancellationToken)
        {
            _businessUserRepositoy.Create(request);

            _eventBus.Publish(new UserCreatedEvent(request.Id, request.Email, request.FirstName, request.MiddelName, request.LastName,
                request.Location, null));

            return Unit.Task;
        }

        public void Handle(CreateBusinessUserCommand command)
        {
            throw new NotImplementedException();
        }

    }
}
