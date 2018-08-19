using MediatR;

namespace SampleSolution.Common.Domain.Commands
{
    public interface ICommandHandler<in T> : IRequestHandler<T>
        where T : ICommand
    {
    }
}
