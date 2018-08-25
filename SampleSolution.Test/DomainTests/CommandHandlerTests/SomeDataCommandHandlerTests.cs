using MediatR;
using Moq;
using System.Threading;
using SampleSolution.Common.Domain.Events;
using SampleSolution.Domain.Commands.CommandHandlers;
using SampleSolution.Domain.Commands.Commands;
using SampleSolution.Domain.Events.Events;
using SampleSolution.Repositories;
using Xunit;

namespace SampleSolution.Test.DomainTests.CommandHandlerTests
{
    public class SomeDataCommandHandlerTests : SomeDataTestBase
    {
        private readonly Mock<IEventBus> _eventBusMock;
        private readonly Mock<ISomeDataWriteRepository> _someDataRepositoryMock;

        public SomeDataCommandHandlerTests()
        {
            _eventBusMock = new Mock<IEventBus>();
            _someDataRepositoryMock = new Mock<ISomeDataWriteRepository>();
        }

        [Fact]
        public void ShouldSaveToDatabaseAndPublishCreatedEventOnHandleCreateSomeDataCommand()
        {
            var createCardCommmand = BuildCreateSomeDataCommand();

            _eventBusMock.Setup(x => x.Publish(It.IsAny<SomeDataCreatedEvent>()))
                .Callback<INotification>(r =>
                {
                    var x = (SomeDataCreatedEvent) r;
                    Assert.Equal(createCardCommmand.FirstName, x.FirstName);
                    Assert.Equal(createCardCommmand.MiddleName, x.MiddleName);
                    Assert.Equal(createCardCommmand.LastName, x.LastName);
                    Assert.Equal(createCardCommmand.Title, x.Title);
                    Assert.Equal(createCardCommmand.CreationDate, x.CreationDate);
                });

            var commandHandler = new SomeDataCommandHandler(_eventBusMock.Object, _someDataRepositoryMock.Object);

            ((IRequestHandler<CreateSomeDataCommand, Unit>) commandHandler).Handle(createCardCommmand, It.IsAny<CancellationToken>());

            _eventBusMock.Verify(x => x.Publish(It.IsAny<SomeDataCreatedEvent>()), Times.Once());
            _someDataRepositoryMock.Verify(x => x.Create(createCardCommmand),Times.Once);
        }

        [Fact]
        public void ShouldRemoveFromDatabaseAndPublishDeletedEventOnHandleDeleteSomeDataCommand()
        {
            var deleteSomeDataCommand = BuildDeleteSomeDataCommand();

            _eventBusMock.Setup(x => x.Publish(It.IsAny<SomeDataDeletedEvent>()))
                .Callback<SomeDataDeletedEvent>(@event =>
                {
                    Assert.Equal(@event.SomeDataId, deleteSomeDataCommand.Id);
                });

            var commandHandler = new SomeDataCommandHandler(_eventBusMock.Object, _someDataRepositoryMock.Object);

            commandHandler.Handle(deleteSomeDataCommand, new CancellationToken());

            _eventBusMock.Verify(x => x.Publish(It.IsAny<SomeDataDeletedEvent>()), Times.Once);
            _someDataRepositoryMock.Verify(x => x.Delete(deleteSomeDataCommand), Times.Once);
        }

        [Fact]
        public void ShouldUpdateDatabaseAndPublishUpdatedEventOnHandleUpdateSomeDataCommand()
        {
            var updateSomeDataCommand = BuildUpdateSomeDataCommand();

            _eventBusMock.Setup(x => x.Publish(It.IsAny<SomeDataUpdatedEvent>()))
                .Callback<SomeDataUpdatedEvent>(@event =>
                {
                    Assert.Equal(@event.SomeDataId, updateSomeDataCommand.SomeDataId);
                    Assert.Equal(@event.Color, updateSomeDataCommand.Color);
                    Assert.Equal(@event.CreationDate, updateSomeDataCommand.CreationDate);
                    Assert.Equal(@event.FacebookUrl, updateSomeDataCommand.FacebookUrl);
                    Assert.Equal(@event.FirstName, updateSomeDataCommand.FirstName);
                    Assert.Equal(@event.LastName, updateSomeDataCommand.LastName);
                    Assert.Equal(@event.MiddleName, updateSomeDataCommand.MiddleName);
                    Assert.Equal(@event.Title, updateSomeDataCommand.Title);
                });

            var commandHandler = new SomeDataCommandHandler(_eventBusMock.Object, _someDataRepositoryMock.Object);

            commandHandler.Handle(updateSomeDataCommand, new CancellationToken());

            _eventBusMock.Verify(x => x.Publish(It.IsAny<SomeDataUpdatedEvent>()), Times.Once);
            _someDataRepositoryMock.Verify(x => x.Update(updateSomeDataCommand), Times.Once);
        }
    }
}
