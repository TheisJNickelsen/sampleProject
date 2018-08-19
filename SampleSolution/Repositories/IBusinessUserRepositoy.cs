using SampleSolution.Data.Contexts.Models;
using SampleSolution.Domain.Commands.Commands;

namespace SampleSolution.Repositories
{
    public interface IBusinessUserRepositoy
    {
        void Create(CreateBusinessUserCommand businessUserCommand);

        BusinessUser GetByApplicationUserId(string applicationUserId);
    }
}
