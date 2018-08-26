using SampleSolution.Domain.Commands.CommandHandlers;
using SampleSolution.Domain.Commands.Commands;
using SampleSolution.Domain.ValueObjects;
using SampleSolution.Repositories;
using Moq;
using System;
using System.Threading;
using MediatR;
using SampleSolution.Common.Domain.Events;
using SampleSolution.Domain.Events.Events;
using Xunit;

namespace SampleSolution.Test.DomainTests.CommandHandlerTests
{
    public class BusinessUserCommandHandlerTests
    {
        private readonly Mock<IEventBus> _eventBusMock;
        private readonly Mock<IBusinessUserRepository> _mySampleSolutionReadRepositoryMock;

        public BusinessUserCommandHandlerTests()
        {
            _eventBusMock = new Mock<IEventBus>();
            _mySampleSolutionReadRepositoryMock = new Mock<IBusinessUserRepository>();
        }

        [Fact]
        public void ShouldSaveToDatabaseOnHandleCreateBusinessUserCommand()
        {
            var createUserCommmand = BuildCreateBusinessUserCommand();

            _eventBusMock.Setup(x => x.Publish(It.IsAny<UserCreatedEvent>()))
                .Callback<INotification>(r =>
                {
                    var x = (UserCreatedEvent)r;
                    Assert.Equal(createUserCommmand.FirstName, x.FirstName);
                    Assert.Equal(createUserCommmand.LastName, x.LastName);
                    Assert.Equal(createUserCommmand.MiddelName, x.MiddleName);
                    Assert.Equal(createUserCommmand.Email, x.Email);
                    Assert.Equal(createUserCommmand.Id, x.UserId);
                    Assert.Equal(createUserCommmand.Location, x.Location);
                });

            var commandHandler = new BusinessUserCommandHandler(_mySampleSolutionReadRepositoryMock.Object, _eventBusMock.Object);

            commandHandler.Handle(createUserCommmand, It.IsAny<CancellationToken>());

            _mySampleSolutionReadRepositoryMock.Verify(x => x.Create(createUserCommmand),Times.Once);
            _eventBusMock.Verify(x => x.Publish(It.IsAny<UserCreatedEvent>()), Times.Once());
        }

        private CreateBusinessUserCommand BuildCreateBusinessUserCommand()
        {
            return new CreateBusinessUserCommand(
                Guid.NewGuid(),
                new IdentityId(Guid.NewGuid().ToString()),
                "TestLocation",
                "TestLocale",
                "TestGender",
                new Email("TestEmail@test.com"),
                "FirstName",
                "MiddleName",
                "LastName");
        }
    }
}
