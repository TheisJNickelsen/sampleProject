using SampleSolution.Domain.Commands.Commands;
using SampleSolution.Domain.ValueObjects;
using SampleSolution.Mappers;
using System;
using Xunit;

namespace SampleSolution.Test.DomainTests.MapperTests
{
    public class BusinessUserMapperTests
    {
        [Fact]
        public void ShouldMapCorrectlyOnCreateBusinessUserCommandToPersistanceModel()
        {
            var command = new CreateBusinessUserCommand(Guid.NewGuid(),
                new IdentityId(Guid.NewGuid().ToString()),
                "SomeLocation",
                "SomeLocale",
                "SomeGender",
                new Email("SomeEmail@some.com"),
                "SomeFirstName",
                "SomeMiddleName",
                "SomeLastName");

            var mapped = BusinessUserMapper.CreateBusinessUserCommandToPersistanceModel(command);

            Assert.Equal(mapped.Id, command.Id);
            Assert.Equal(mapped.Gender, command.Gender);
            Assert.Equal(mapped.IdentityId, command.IdentityId.Value);
            Assert.Equal(mapped.Locale, command.Locale);
            Assert.Equal(mapped.Location, command.Location);
        }

    }
}
