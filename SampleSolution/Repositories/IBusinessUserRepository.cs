using SampleSolution.Data.Contexts;
using SampleSolution.Data.Contexts.Models;
using SampleSolution.Domain.Commands.Commands;
using SampleSolution.Domain.ValueObjects;

namespace SampleSolution.Repositories
{
    public interface IBusinessUserRepository
    {
        void Create(CreateBusinessUserCommand businessUserCommand);

        BusinessUser GetByApplicationUserId(ApplicationUserId id);
        BusinessUser GetByApplicationUserId(ApplicationUserId id, SomeDataContext dbContext);
    }
}
