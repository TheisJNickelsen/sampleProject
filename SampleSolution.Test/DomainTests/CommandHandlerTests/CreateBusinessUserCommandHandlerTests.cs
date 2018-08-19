using SampleSolution.Domain.Commands.CommandHandlers;
using SampleSolution.Domain.Commands.Commands;
using SampleSolution.Domain.ValueObjects;
using SampleSolution.Repositories;
using Moq;
using System;
using System.Threading;
using Xunit;

namespace SampleSolution.Test.DomainTests.CommandHandlerTests
{
    public class CreateBusinessUserCommandHandlerTests
    {
        private readonly Mock<IBusinessUserRepositoy> _mySampleSolutionReadRepositoryMock;

        public CreateBusinessUserCommandHandlerTests()
        {
            _mySampleSolutionReadRepositoryMock = new Mock<IBusinessUserRepositoy>();
        }

        [Fact]
        public void ShouldSaveToDatabaseOnHandleCreateBusinessUserCommand()
        {
            var createCardCommmand = BuildCreateBusinessUserCommand();

            var commandHandler = new BusinessUserCommandHandler(_mySampleSolutionReadRepositoryMock.Object);

            commandHandler.Handle(createCardCommmand, It.IsAny<CancellationToken>());

            _mySampleSolutionReadRepositoryMock.Verify(x => x.Create(createCardCommmand),Times.Once);
        }

        private CreateBusinessUserCommand BuildCreateBusinessUserCommand()
        {
            return new CreateBusinessUserCommand(
                Guid.NewGuid(),
                new IdentityId(Guid.NewGuid().ToString()),
                "TestLocation",
                "TestLocale",
                "TestGender");
        }
    }
}
