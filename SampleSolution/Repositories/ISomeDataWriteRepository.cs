using System;
using System.Collections.Generic;
using SampleSolution.Common.UnitOfWork;
using SampleSolution.Data.Contexts.Models;
using SampleSolution.Domain.Aggregates;
using SampleSolution.Domain.Commands.Commands;

namespace SampleSolution.Repositories
{
    public interface ISomeDataWriteRepository 
    {
        SomeAggregate Get(Guid someDataId);
        void Create(SomeAggregate someData);
        //void CopyToNewUser(ShareContactCommand shareContactCommand);
        void Delete(SomeAggregate someData);
        void Save(SomeAggregate someData);

        List<SomeAggregate> GetSomeData(string userEmail);
    }
}
