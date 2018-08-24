using SampleSolution.Data.Contexts.Models;
using SampleSolution.Domain.Events.Events;
using SampleSolution.DTOs;
using SampleSolution.Models.ElasticSearch;

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

        public static UserStream UserCreatedEventToStream(UserCreatedEvent @event)
        {
            return new UserStream(@event.UserId,
                @event.Email, 
                @event.FirstName, 
                @event.MiddleName, 
                @event.LastName, 
                @event.ImagePath);
        }
    }
}
