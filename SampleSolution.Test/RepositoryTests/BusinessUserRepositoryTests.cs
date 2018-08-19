using System;
using SampleSolution.Data.Contexts;
using SampleSolution.Data.Contexts.Models;
using SampleSolution.Test.DomainTests;
using Moq;
using System.Collections.Generic;
using System.Linq;
using SampleSolution.Domain.Commands.Commands;
using SampleSolution.Domain.ValueObjects;
using SampleSolution.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;
using SampleSolution.Mappers;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace SampleSolution.Test.RepositoryTests
{

    public class BusinessUserRepositoryTests
    {
        [Fact]
        public void CreateShouldAppendToDatabaseAndSaveChanges()
        {
            var businessUserDb = new List<BusinessUser>();
            var mockSet = new MockDbSet<BusinessUser>(businessUserDb);
            var contextMock = new Mock<SomeDataContext>();

            contextMock.Setup(c => c.BusinessUsers).Returns(mockSet.Object);

            var createBusinessUserCommand = new CreateBusinessUserCommand(Guid.NewGuid(), 
                new IdentityId("SomeId"),
                "SomeLocation",
                "SomeLocale",
                "SomeGender"
            );
            var businessUserRepository = new BusinessUserRepository(contextMock.Object);

            businessUserRepository.Create(createBusinessUserCommand);

            contextMock.Verify(x => x.SaveChanges(), Times.Once);
            Assert.Single(businessUserDb);
        }
    }
}
