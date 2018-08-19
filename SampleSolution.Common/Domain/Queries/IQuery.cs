using MediatR;

namespace SampleSolution.Common.Domain.Queries
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}
