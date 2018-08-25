using SampleSolution.Domain.Aggregates;
using System.Collections.Generic;

namespace SampleSolution.Repositories
{
    public interface ISomeDataReadRepository
    {
        List<SomeAggregate> GetSomeData(string userEmail);
    }
}
