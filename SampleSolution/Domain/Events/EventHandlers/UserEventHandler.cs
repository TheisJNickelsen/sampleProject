using System;
using System.Threading;
using System.Threading.Tasks;
using SampleSolution.Common.Domain.Events;
using SampleSolution.Domain.Events.Events;
using Marten;
using SampleSolution.Repositories;

namespace SampleSolution.Domain.Events.EventHandlers
{
    public class UserEventHandler : 
        IEventHandler<UserCreatedEvent>
    {
        private readonly IUserStreamWriteRepository _userStreamWriteRepository;

        public UserEventHandler(IUserStreamWriteRepository userStreamWriteRepository)
        {
            _userStreamWriteRepository = userStreamWriteRepository ?? throw new ArgumentNullException(nameof(userStreamWriteRepository));
        }

        public Task Handle(UserCreatedEvent @event, CancellationToken cancellationToken)
        {
            _userStreamWriteRepository.Add(@event);
            return Task.CompletedTask;
        }
    }
}
