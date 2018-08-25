using SampleSolution.Data.Contexts;
using SampleSolution.Domain.Aggregates;
using SampleSolution.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleSolution.Repositories
{
    public class SomeDataReadRepository : ISomeDataReadRepository
    {
        public SomeDataReadRepository(SomeDataContext someDataEntities)
        {
            SomeDataEntities = someDataEntities;
        }

        public SomeDataContext SomeDataEntities { get; }

        public List<SomeAggregate> GetSomeData(string userEmail)
        {
            try
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
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
