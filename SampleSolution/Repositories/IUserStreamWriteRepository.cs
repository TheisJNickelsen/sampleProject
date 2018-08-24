using SampleSolution.Domain.Events.Events;

namespace SampleSolution.Repositories
{
    public interface IUserStreamWriteRepository
    {
        void Add(UserCreatedEvent userCreatedEvent);
    }
}
