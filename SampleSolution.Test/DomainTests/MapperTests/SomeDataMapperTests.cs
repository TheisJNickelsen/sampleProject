using System;
using SampleSolution.Mappers;
using Xunit;

namespace SampleSolution.Test.DomainTests.MapperTests
{
    public class SomeDataMapperTests : SomeDataTestBase
    {
        [Fact]
        public void ShouldMapCorrectlyOnSomeAggregateToPersistanceModel()
        {
            var aggregate = BuildSomeAggregate();
            var mapped = SomeDataMapper.SomeAggregateToPersistanceModel(aggregate);

            Assert.Equal(aggregate.CreationDate, mapped.CreationDate);
            Assert.Equal(aggregate.FacebookUrl.Value,mapped.FacebookUrl);
            Assert.Equal(aggregate.Color.Value, mapped.Color);
            Assert.Equal(aggregate.FirstName, mapped.FirstName);
            Assert.Equal(aggregate.Id, mapped.Id);
            Assert.Equal(aggregate.LastName, mapped.LastName);
            Assert.Equal(aggregate.MiddleName, mapped.MiddleName);
            Assert.Equal(aggregate.Title, mapped.Title);
            Assert.Equal(aggregate.BusinessUserId, mapped.BusinessUserId);
        }

        [Fact]
        public void ShouldMapCorrectlyOnPersistanceModelToAggregateRoot()
        {
            var someData = BuildSomeDataPersistanceModel();
            var aggregateRoot = SomeDataMapper.PersistanceModelToAggregateRoot(someData);

            Assert.Equal(aggregateRoot.CreationDate, someData.CreationDate);
            Assert.Equal(aggregateRoot.FacebookUrl.Value, someData.FacebookUrl);
            Assert.Equal(aggregateRoot.Color.Value, someData.Color);
            Assert.Equal(aggregateRoot.FirstName, someData.FirstName);
            Assert.Equal(aggregateRoot.Id, someData.Id);
            Assert.Equal(aggregateRoot.LastName, someData.LastName);
            Assert.Equal(aggregateRoot.MiddleName, someData.MiddleName);
            Assert.Equal(aggregateRoot.Title, someData.Title);
        }
    }
}
