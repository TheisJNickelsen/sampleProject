using SampleSolution.DTOs;
using SampleSolution.Mappers;
using Xunit;

namespace SampleSolution.Test.DomainTests.MapperTests
{
    public class ApplicationUserMapperTests
    {
        [Fact]
        public void ShouldMapCorrectlyOnCreateSomeDataCommandToPersistanceModel()
        {
            var dto = new RegistrationDto
            {
                Email = "someEmail@gmail.com",
                FirstName = "SomeFirstName",
                LastName = "SomeLastname",
                Location = "SomeLocation",
                Password = "SomePassword"
            };
            var mapped = ApplicationUserMapper.RegistrationDtoToApplicationUser(dto);

            Assert.Equal(dto.FirstName, mapped.FirstName);
            Assert.Equal(dto.Email, mapped.Email);
            Assert.Equal(dto.LastName, mapped.LastName);
            Assert.Equal(mapped.UserName, dto.Email);
        }

    }
}
