using System;
using System.Collections.Generic;
using SampleSolution.Domain.Aggregates;
using SampleSolution.Domain.Commands.Commands;

namespace SampleSolution.Repositories
{
    public interface ISomeDataRepository
    {
        void Create(CreateSomeDataCommand createSomeDataCommand);
        void Delete(DeleteSomeDataCommand id);
        void Update(UpdateSomeDataCommand updateSomeDataCommand);

        List<SomeAggregate> GetMySampleSolution(string userEmail);
    }
}
