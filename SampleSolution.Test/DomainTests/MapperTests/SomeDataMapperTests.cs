using System;
using SampleSolution.Mappers;
using Xunit;

namespace SampleSolution.Test.DomainTests.MapperTests
{
    public class SomeDataMapperTests : SomeDataTestBase
    {
        [Fact]
        public void ShouldMapCorrectlyOnCreateSomeDataCommandToPersistanceModel()
        {
            var createSomeDataCommand = BuildCreateSomeDataCommand();
            var businessUserId = Guid.NewGuid();
            var mapped = SomeDataMapper.CreateSomeDataCommandToPersistanceModel(createSomeDataCommand, businessUserId);

            Assert.Equal(createSomeDataCommand.CreationDate, mapped.CreationDate);
            Assert.Equal(createSomeDataCommand.FacebookUrl.Value,mapped.FacebookUrl);
            Assert.Equal(createSomeDataCommand.Color.Value, mapped.Color);
            Assert.Equal(createSomeDataCommand.FirstName, mapped.FirstName);
            Assert.Equal(createSomeDataCommand.Id, mapped.Id);
            Assert.Equal(createSomeDataCommand.LastName, mapped.LastName);
            Assert.Equal(createSomeDataCommand.MiddleName, mapped.MiddleName);
            Assert.Equal(createSomeDataCommand.Title, mapped.Title);
            Assert.Equal(businessUserId, mapped.BusinessUserId);
        }

        [Fact]
        public void ShouldMapCorrectlyOnPersistanceModelToAggregateRoot()
        {
            var SomeData = BuildSomeDataPersistanceModel();
            var aggregateRoot = SomeDataMapper.PersistanceModelToAggregateRoot(SomeData);

            Assert.Equal(aggregateRoot.CreationDate, SomeData.CreationDate);
            Assert.Equal(aggregateRoot.FacebookUrl.Value, SomeData.FacebookUrl);
            Assert.Equal(aggregateRoot.Color.Value, SomeData.Color);
            Assert.Equal(aggregateRoot.FirstName, SomeData.FirstName);
            Assert.Equal(aggregateRoot.Id, SomeData.Id);
            Assert.Equal(aggregateRoot.LastName, SomeData.LastName);
            Assert.Equal(aggregateRoot.MiddleName, SomeData.MiddleName);
            Assert.Equal(aggregateRoot.Title, SomeData.Title);
        }
    }
}
