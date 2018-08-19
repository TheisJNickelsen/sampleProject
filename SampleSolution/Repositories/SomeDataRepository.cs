using SampleSolution.Data.Contexts;
using SampleSolution.Domain.Aggregates;
using SampleSolution.Domain.Commands.Commands;
using SampleSolution.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleSolution.Repositories
{
    public class SomeDataRepository : ISomeDataRepository
    {
        public SomeDataRepository(SomeDataContext someDataEntities)
        {
            SomeDataEntities = someDataEntities;
        }

        public SomeDataContext SomeDataEntities { get; }

        public void Create(CreateSomeDataCommand createSomeDataCommand)
        {
            try
            {
                using (var context = SomeDataEntities)
                {
                    var businessUser = context.BusinessUsers
                        .First(u => u.Identity.Email.Equals(createSomeDataCommand.UserEmail));
                    var someDatatoSave = SomeDataMapper.CreateSomeDataCommandToPersistanceModel(createSomeDataCommand, businessUser.Id);
                    context.SomeData.Add(someDatatoSave);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void Delete(DeleteSomeDataCommand deleteSomeDataCommand)
        {
            using (var context = SomeDataEntities)
            {
                var someDataToDelete = context.SomeData.FirstOrDefault(k => k.Id == deleteSomeDataCommand.Id);

                if (someDataToDelete != null)
                {
                    context.SomeData.Remove(someDataToDelete);
                    context.SaveChanges();
                }
            }
        }

        public List<SomeAggregate> GetMySampleSolution(string userEmail)
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

        public void Update(UpdateSomeDataCommand updateSomeDataCommand)
        {
            using (var context = SomeDataEntities)
            {
                var someDataToUpdate = context.SomeData.FirstOrDefault(k => k.Id == updateSomeDataCommand.SomeDataId);

                if (someDataToUpdate != null)
                {
                    someDataToUpdate.Color = updateSomeDataCommand.Color.Value;
                    someDataToUpdate.CreationDate = updateSomeDataCommand.CreationDate;
                    someDataToUpdate.FacebookUrl = updateSomeDataCommand.FacebookUrl.Value;
                    someDataToUpdate.FirstName = updateSomeDataCommand.FirstName;
                    someDataToUpdate.LastName = updateSomeDataCommand.LastName;
                    someDataToUpdate.MiddleName = updateSomeDataCommand.MiddleName;
                    someDataToUpdate.Title = updateSomeDataCommand.Title;

                    context.SaveChanges();
                }
            }
        }
    }
}
