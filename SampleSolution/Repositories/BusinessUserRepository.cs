using SampleSolution.Data.Contexts;
using SampleSolution.Domain.Commands.Commands;
using SampleSolution.Mappers;
using System;
using System.Linq;
using SampleSolution.Data.Contexts.Models;

namespace SampleSolution.Repositories
{
    public class BusinessUserRepository : IBusinessUserRepositoy
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

        public BusinessUser GetByApplicationUserId(string applicationUserId)
        {
            using (var context = _someDataContext)
            {
                return context.BusinessUsers.FirstOrDefault(u => u.IdentityId == applicationUserId);
            }
        }
    }
}
