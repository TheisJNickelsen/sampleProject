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
                    return SomeDataMapper.PersistanceModelToAggregateRoot(
                        context.SomeData.FirstOrDefault(d => d.Id == someDataId));
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
                    var businessUser =
                        context.BusinessUsers.FirstOrDefault(u => u.Id == someData.BusinessUserId);

                    var someDatatoSave = SomeDataMapper.SomeAggregateToPersistanceModel(someData);
                    context.SomeData.Add(someDatatoSave);
                    //context.SaveChanges();
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
                    var someDataToDelete = context.SomeData.FirstOrDefault(d => d.Id == someData.Id);

                    if (someDataToDelete != null)
                    {
                        context.SomeData.Remove(someDataToDelete);
                        context.SaveChanges();
                    }
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
                    var someDataToUpdate = context.SomeData.FirstOrDefault(d => d.Id == someData.Id);

                    if (someDataToUpdate != null)
                    {
                        someDataToUpdate.Color = someData.Color.Value;
                        someDataToUpdate.CreationDate = someData.CreationDate;
                        someDataToUpdate.FacebookUrl = someData.FacebookUrl.Value;
                        someDataToUpdate.FirstName = someData.FirstName;
                        someDataToUpdate.LastName = someData.LastName;
                        someDataToUpdate.MiddleName = someData.MiddleName;
                        someDataToUpdate.Title = someData.Title;

                        context.SaveChanges();
                    }
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

            if (someDataToUpdate != null)
            {
                someDataToUpdate.Color = someData.Color.Value;
                someDataToUpdate.CreationDate = someData.CreationDate;
                someDataToUpdate.FacebookUrl = someData.FacebookUrl.Value;
                someDataToUpdate.FirstName = someData.FirstName;
                someDataToUpdate.LastName = someData.LastName;
                someDataToUpdate.MiddleName = someData.MiddleName;
                someDataToUpdate.Title = someData.Title;
            }
        }

        //public void CopyToNewUser(ShareContactCommand shareContactCommand)
        //{
        //    try
        //    {
        //        using (var context = SomeDataEntities)
        //        {
        //            var contactToCopy =
        //                context.SomeData.FirstOrDefault(k => k.Id == shareContactCommand.ContactId);

        //            if (contactToCopy != null)
        //            {
        //                var copyWithNewOwner = new SomeData
        //                {
        //                    BusinessUserId = shareContactCommand.RecipientUserId,
        //                    Color = contextToCreateWithNewId
        //                };


        //                context.SomeData.Add(copyWithNewOwner);
        //            }

        //            context.SaveChanges();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //        throw;
        //    }
        //}

        public List<SomeAggregate> GetSomeData(string userEmail)
        {
            using (var context = SomeDataEntities)
            {
                var businessUser = context.BusinessUsers.FirstOrDefault(bu => bu.Identity.Email == userEmail);
                var mySampleSolution = context.SomeData
                    .Where(k => k.BusinessUserId == businessUser.Id)
                    .Select(k => SomeDataMapper.PersistanceModelToAggregateRoot(k)).ToList();
                return mySampleSolution;
            }
        }
    }
}
