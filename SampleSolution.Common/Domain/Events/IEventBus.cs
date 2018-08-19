using System.Threading.Tasks;

namespace SampleSolution.Common.Domain.Events
{
    public interface IEventBus
    {
        Task Publish<TEvent>(TEvent @event) where TEvent : IEvent;
    }
}
