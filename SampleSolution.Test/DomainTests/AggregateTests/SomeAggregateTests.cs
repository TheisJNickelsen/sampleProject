using System;
using SampleSolution.Domain.Commands.Commands;
using SampleSolution.Domain.ValueObjects;
using Xunit;

namespace SampleSolution.Test.DomainTests.AggregateTests
{
    public class SomeAggregateTests : SomeDataTestBase
    {
        [Fact]
        public void ShouldCreateCopyWithDifferentIdAndBusinessUserOnSendTo()
        {
            var newUserId = Guid.NewGuid();
            var aggregate = BuildSomeAggregate();

            var copy = aggregate.SendTo(newUserId);

            Assert.Equal(copy.LastName, aggregate.LastName);
            Assert.Equal(copy.BusinessUserId, newUserId);
            Assert.NotEqual(copy.BusinessUserId, aggregate.BusinessUserId);
            Assert.Equal(copy.FirstName, aggregate.FirstName);
            Assert.Equal(copy.MiddleName, aggregate.MiddleName);
            Assert.Equal(copy.Color, aggregate.Color);
            Assert.Equal(copy.CreationDate, aggregate.CreationDate);
            Assert.Equal(copy.FacebookUrl, aggregate.FacebookUrl);
            Assert.Equal(copy.Title, aggregate.Title);
            Assert.NotEqual(copy.Id, aggregate.Id);
        }

        [Fact]
        public void ShouldAttributesOnChangeFields()
        {
            var aggregate = BuildSomeAggregate();
            var updateCommand = new UpdateSomeDataCommand(aggregate.Id,
                "NewFirstName",
                "NewMiddleName",
                "NewLastName",
                "NewTitle",
                new Color("#123"),
                DateTime.Now,
                new FacebookUrl("www.facebook.com/someuser"));

            aggregate.ChangeFields(updateCommand);

            Assert.Equal(updateCommand.LastName, aggregate.LastName);
            Assert.Equal(updateCommand.FirstName, aggregate.FirstName);
            Assert.Equal(updateCommand.MiddleName, aggregate.MiddleName);
            Assert.Equal(updateCommand.Color, aggregate.Color);
            Assert.Equal(updateCommand.CreationDate, aggregate.CreationDate);
            Assert.Equal(updateCommand.FacebookUrl, aggregate.FacebookUrl);
            Assert.Equal(updateCommand.Title, aggregate.Title);
        }
    }
}
