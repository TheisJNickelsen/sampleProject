using SampleSolution.Data.Contexts.Models;
using SampleSolution.Domain.Commands.Commands;

namespace SampleSolution.Mappers
{
    public static class BusinessUserMapper
    {
        public static BusinessUser CreateBusinessUserCommandToPersistanceModel(CreateBusinessUserCommand command)
        {
            return new BusinessUser
            {
                Id = command.Id,
                Gender = command.Gender,
                Location = command.Location,
                IdentityId = command.IdentityId.Value,
                Locale = command.Locale
            };
        }
    }
}
