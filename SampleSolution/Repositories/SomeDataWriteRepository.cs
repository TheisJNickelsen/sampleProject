using SampleSolution.Data.Contexts;
using SampleSolution.Domain.Aggregates;
using SampleSolution.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleSolution.Repositories
{
    public class SomeDataWriteRepository : ISomeDataWriteRepository
    {
        public SomeDataWriteRepository(SomeDataContext someDataEntities)
        {
            SomeDataEntities = someDataEntities;
        }

        public SomeDataContext SomeDataEntities { get; }

        public SomeAggregate Get(Guid someDataId)
        {
            try
            {
                using (var context = SomeDataEntities)
                {
                    return Get(someDataId, context);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public SomeAggregate Get(Guid someDataId, SomeDataContext dbContext)
        {
            return SomeDataMapper.PersistanceModelToAggregateRoot(
                dbContext.SomeData.FirstOrDefault(d => d.Id == someDataId));
        }

        public void Create(SomeAggregate someData)
        {
            try
            {
                using (var context = SomeDataEntities)
                {
                    Create(someData, context);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void Create(SomeAggregate someData, SomeDataContext dbContext)
        {
            var someDatatoSave = SomeDataMapper.SomeAggregateToPersistanceModel(someData);
            dbContext.SomeData.Add(someDatatoSave);
        }

        public void Delete(SomeAggregate someData)
        {
            try
            {
                using (var context = SomeDataEntities)
                {
                    Delete(someData, context);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void Delete(SomeAggregate someData, SomeDataContext dbContext)
        {
            var someDataToDelete = dbContext.SomeData.FirstOrDefault(d => d.Id == someData.Id);

            if (someDataToDelete != null)
            {
                dbContext.SomeData.Remove(someDataToDelete);
            }
        }

        public void Save(SomeAggregate someData)
        {
            try
            {
                using (var context = SomeDataEntities)
                {
                    Save(someData);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void Save(SomeAggregate someData, SomeDataContext dbContext)
        {
            var someDataToUpdate = dbContext.SomeData.FirstOrDefault(d => d.Id == someData.Id);

            if (someDataToUpdate == null) return;
            someDataToUpdate.Color = someData.Color.Value;
            someDataToUpdate.CreationDate = someData.CreationDate;
            someDataToUpdate.FacebookUrl = someData.FacebookUrl.Value;
            someDataToUpdate.FirstName = someData.FirstName;
            someDataToUpdate.LastName = someData.LastName;
            someDataToUpdate.MiddleName = someData.MiddleName;
            someDataToUpdate.Title = someData.Title;
            someDataToUpdate.BusinessUserId = someData.BusinessUserId;
        }
    }
}
