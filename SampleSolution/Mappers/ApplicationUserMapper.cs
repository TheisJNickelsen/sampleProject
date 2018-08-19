using SampleSolution.Data.Contexts.Models;
using SampleSolution.DTOs;

namespace SampleSolution.Mappers
{
    public static class ApplicationUserMapper
    {
        public static ApplicationUser RegistrationDtoToApplicationUser(RegistrationDto dto)
        {
            return new ApplicationUser
            {
                LastName = dto.LastName,
                FirstName = dto.FirstName,
                Email = dto.Email,
                UserName = dto.Email
            };
        }
    }
}
