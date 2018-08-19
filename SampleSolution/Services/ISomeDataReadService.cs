using System.Collections.Generic;
using SampleSolution.Domain.Aggregates;

namespace SampleSolution.Services
{
    public interface ISomeDataReadService
    {
        List<SomeAggregate> GetSomeData(string userId);
    }
}
