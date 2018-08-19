using System;

namespace SampleSolution.Common.Domain.Aggregates
{
    public interface IAggregate
    {
        Guid Id { get; }
    }
}
