using SampleSolution.Data.Contexts;
using SampleSolution.Data.Contexts.Models;
using SampleSolution.Domain.Commands.Commands;
using SampleSolution.Domain.ValueObjects;
using SampleSolution.Mappers;
using SampleSolution.Repositories;
using SampleSolution.Test.DomainTests;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Marten;
using Xunit;
using Microsoft.EntityFrameworkCore;
using SampleSolution.Domain.Aggregates;
using SampleSolution.Test.Util;

namespace SampleSolution.Test.RepositoryTests
{
    public class SomeDataRepositoryTests : SomeDataTestBase
    {
        [Fact]
        public void CreateSomeDataShouldAppendToDatabaseAndSaveChanges()
        {
            var someDataDb = new List<SomeData>();
            var businessUser = BuildBusinessUser();
            var businessUserDb = new List<BusinessUser>
            {
                businessUser
            };
            var businessUserMockSet = new MockDbSet<BusinessUser>(businessUserDb);
            var mockSet = new MockDbSet<SomeData>(someDataDb);
            var contextMock = new Mock<SomeDataContext>();

            contextMock.Setup(c => c.BusinessUsers).Returns(businessUserMockSet.Object);
            contextMock.Setup(c => c.SomeData).Returns(mockSet.Object);

            var someAggregate = BuildSomeAggregate();

            var someDataRepository = new SomeDataWriteRepository(contextMock.Object);

            someDataRepository.Create(someAggregate);

            contextMock.Verify(x => x.SaveChanges(), Times.Once);
            Assert.Single(someDataDb);
        }

        [Fact]
        public void SaveSomeDataShoulUpdateSomeDataInDatabaseAndSaveChanges()
        {
            var updateSomeDataCommand = BuildUpdateSomeDataCommand();
            var businessUser = new BusinessUser
            {
                Id = Guid.NewGuid(),
                Gender = "SomeGender",
                Locale = "SomeLocale",
                Location = "SomeLocation",
                Identity = new ApplicationUser
                {
                    Email = "someemail@gmail.com"
                }
            };
            var someDataDb = SomeDataMapper.UpdateSomeDataCommandToPersistanceModel(updateSomeDataCommand);
            someDataDb.BusinessUserId = businessUser.Id;

            var someDataDbSet = new List<SomeData>
            {
                someDataDb
            };
            var businessUserDb = new List<BusinessUser>
            {
                businessUser
            };

            var businessUserMockSet = new MockDbSet<BusinessUser>(businessUserDb);
            var someDataMockSet = new MockDbSet<SomeData>(someDataDbSet);
            var contextMock = new Mock<SomeDataContext>();
            
            contextMock.Setup(c => c.SomeData).Returns(someDataMockSet.Object);
            contextMock.Setup(c => c.BusinessUsers).Returns(businessUserMockSet.Object);

            var someDataRepository = new SomeDataWriteRepository(contextMock.Object);

            var updatedSomeData = SomeAggregate.Create(updateSomeDataCommand.SomeDataId,
                "NewFirstName",
                "NewMiddleName",
                "NewLastName",
                "NewTitle",
                new Color("#c1d0c3"),
                DateTime.Now,
                new FacebookUrl(null),
                Guid.NewGuid());

            someDataRepository.Save(updatedSomeData);
            var updatedDb = someDataRepository.Get(updateSomeDataCommand.SomeDataId);

            contextMock.Verify(x => x.SaveChanges(), Times.Once);
            Assert.Equal(updatedDb.Id, updatedSomeData.Id);
            Assert.Equal(updatedDb.Color, updatedSomeData.Color);
            Assert.Equal(updatedDb.CreationDate, updatedSomeData.CreationDate);
            Assert.Equal(updatedDb.FacebookUrl, updatedSomeData.FacebookUrl);
            Assert.Equal(updatedDb.FirstName, updatedSomeData.FirstName);
            Assert.Equal(updatedDb.LastName, updatedSomeData.LastName);
            Assert.Equal(updatedDb.MiddleName, updatedSomeData.MiddleName);
            Assert.Equal(updatedDb.Title, updatedSomeData.Title);
        }

        [Fact]
        public void DeleteSomeDataShouldDeleteFromDatabaseAndSaveChanges()
        {
            var someDataDb = BuildSomeDataPersistanceModel();
            var sampleSolutionDb = new List<SomeData>
            {
                someDataDb
            };

            var mockSet = new MockDbSet<SomeData>(sampleSolutionDb);
            var contextMock = new Mock<SomeDataContext>();

            contextMock.Setup(c => c.SomeData).Returns(mockSet.Object);
            
            var someDataRepository = new SomeDataWriteRepository(contextMock.Object);

            var someAggregate = BuildSomeAggregate(someDataDb);

            someDataRepository.Delete(someAggregate);

            contextMock.Verify(x => x.SaveChanges(), Times.Once);
            Assert.Empty(sampleSolutionDb);
        }
    }
}
