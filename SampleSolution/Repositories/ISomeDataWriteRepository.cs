using System;
using System.Collections.Generic;
using SampleSolution.Common.UnitOfWork;
using SampleSolution.Data.Contexts;
using SampleSolution.Data.Contexts.Models;
using SampleSolution.Domain.Aggregates;
using SampleSolution.Domain.Commands.Commands;

namespace SampleSolution.Repositories
{
    public interface ISomeDataWriteRepository 
    {
        SomeAggregate Get(Guid someDataId);
        SomeAggregate Get(Guid someDataId, SomeDataContext dbContext);
        void Create(SomeAggregate someData);
        void Create(SomeAggregate someData, SomeDataContext dbContext);
        //void CopyToNewUser(ShareContactCommand shareContactCommand);
        void Delete(SomeAggregate someData);
        void Delete(SomeAggregate someData, SomeDataContext dbContext);
        void Save(SomeAggregate someData);
        void Save(SomeAggregate someData, SomeDataContext dbContext);

        List<SomeAggregate> GetSomeData(string userEmail);
    }
}
