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

namespace SampleSolution.Test.RepositoryTests
{
    public class SomeDataRepositoryTests : SomeDataTestBase
    {
        [Fact]
        public void CreateSampleSolutionhouldAppendToDatabaseAndSaveChanges()
        {
            var SampleSolutionDb = new List<SomeData>();
            var businessUser = BuildBusinessUser();
            var businessUserDb = new List<BusinessUser>
            {
                businessUser
            };
            var businessUserMockSet = new MockDbSet<BusinessUser>(businessUserDb);
            var mockSet = new MockDbSet<SomeData>(SampleSolutionDb);
            var contextMock = new Mock<SomeDataContext>();

            contextMock.Setup(c => c.BusinessUsers).Returns(businessUserMockSet.Object);
            contextMock.Setup(c => c.SomeData).Returns(mockSet.Object);

            var createSomeDataCommand = BuildCreateSomeDataCommand();

            var someDataRepository = new SomeDataRepository(contextMock.Object);

            someDataRepository.Create(createSomeDataCommand);

            contextMock.Verify(x => x.SaveChanges(), Times.Once);
            Assert.Single(SampleSolutionDb);
        }

        [Fact]
        public void UpdateSampleSolutionhoulUpdateSomeDataInDatabaseAndSaveChanges()
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

            var someDataRepository = new SomeDataRepository(contextMock.Object);

            var updatedSomeData = new UpdateSomeDataCommand(updateSomeDataCommand.SomeDataId,
                "NewFirstName",
                "NewMiddleName",
                "NewLastName",
                "NewTitle",
                new Color("#c1d0c3"),
                DateTime.Now,
                new FacebookUrl(null));

            someDataRepository.Update(updatedSomeData);
            var updatedDb = someDataRepository.GetMySampleSolution(businessUser.Identity.Email).First();

            contextMock.Verify(x => x.SaveChanges(), Times.Once);
            Assert.Equal(updatedDb.Id, updatedSomeData.SomeDataId);
            Assert.Equal(updatedDb.Color, updatedSomeData.Color);
            Assert.Equal(updatedDb.CreationDate, updatedSomeData.CreationDate);
            Assert.Equal(updatedDb.FacebookUrl, updatedSomeData.FacebookUrl);
            Assert.Equal(updatedDb.FirstName, updatedSomeData.FirstName);
            Assert.Equal(updatedDb.LastName, updatedSomeData.LastName);
            Assert.Equal(updatedDb.MiddleName, updatedSomeData.MiddleName);
            Assert.Equal(updatedDb.Title, updatedSomeData.Title);
        }

        [Fact]
        public void DeleteMySampleSolutionhouldDeleteFromDatabaseAndSaveChanges()
        {
            var someDataDb = BuildSomeDataPersistanceModel();
            var sampleSolutionDb = new List<SomeData>
            {
                someDataDb
            };

            var mockSet = new MockDbSet<SomeData>(sampleSolutionDb);
            var contextMock = new Mock<SomeDataContext>();

            contextMock.Setup(c => c.SomeData).Returns(mockSet.Object);
            
            var someDataRepository = new SomeDataRepository(contextMock.Object);

            someDataRepository.Delete(new DeleteSomeDataCommand(someDataDb.Id));

            contextMock.Verify(x => x.SaveChanges(), Times.Once);
            Assert.Empty(sampleSolutionDb);
        }
    }
}
