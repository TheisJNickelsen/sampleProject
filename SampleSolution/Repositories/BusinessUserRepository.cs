using SampleSolution.Data.Contexts;
using SampleSolution.Domain.Commands.Commands;
using SampleSolution.Mappers;
using System;
using System.Linq;
using SampleSolution.Data.Contexts.Models;
using SampleSolution.Domain.ValueObjects;

namespace SampleSolution.Repositories
{
    public class BusinessUserRepository : IBusinessUserRepository
    {
        private readonly SomeDataContext _someDataContext;

        public BusinessUserRepository(SomeDataContext someDataContext)
        {
            _someDataContext = someDataContext ?? throw new ArgumentNullException(nameof(someDataContext));
        }

        public void Create(CreateBusinessUserCommand businessUserCommand)
        {
            using (var context = _someDataContext)
            {
                var businessUser = BusinessUserMapper.CreateBusinessUserCommandToPersistanceModel(businessUserCommand);
                context.BusinessUsers.Add(businessUser);
                context.SaveChanges();
            }
        }

        public BusinessUser GetByApplicationUserId(ApplicationUserId id)
        {
            return _someDataContext.BusinessUsers.FirstOrDefault(u => u.IdentityId == id.Value);
        }

        public BusinessUser GetByApplicationUserId(ApplicationUserId id, SomeDataContext dbContext)
        {
            return dbContext.BusinessUsers.FirstOrDefault(u => u.IdentityId == id.Value);
        }
    }
}
