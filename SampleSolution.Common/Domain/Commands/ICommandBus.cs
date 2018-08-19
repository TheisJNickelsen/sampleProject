using System.Threading.Tasks;

namespace SampleSolution.Common.Domain.Commands
{
    public interface ICommandBus
    {
        Task Send<TCommand>(TCommand command) where TCommand : ICommand;
    }
}
