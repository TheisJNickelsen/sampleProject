using SampleSolution.Domain.Commands.Commands;

namespace SampleSolution.Repositories
{
    public interface IBusinessUserRepositoy
    {
        void Create(CreateBusinessUserCommand businessUserCommand);
    }
}
