using System;
using System.Collections.Generic;
using MediatR;
using Moq;
using System.Threading;
using SampleSolution.Common.Domain.Events;
using SampleSolution.Data.Contexts;
using SampleSolution.Data.Contexts.Models;
using SampleSolution.Domain.Aggregates;
using SampleSolution.Domain.Commands.CommandHandlers;
using SampleSolution.Domain.Commands.Commands;
using SampleSolution.Domain.Events.Events;
using SampleSolution.Domain.ValueObjects;
using SampleSolution.Repositories;
using SampleSolution.Test.Util;
using Xunit;

namespace SampleSolution.Test.DomainTests.CommandHandlerTests
{
    public class SomeDataCommandHandlerTests : SomeDataTestBase
    {
        private readonly Mock<IEventBus> _eventBusMock;
        private readonly Mock<ISomeDataWriteRepository> _someDataWriteRepositoryMock;
        private readonly Mock<IBusinessUserRepository> _businessUserRepositoryMock;
        private readonly Mock<SomeDataContext> _dbContextMock;

        private BusinessUser _businessUser;

        public SomeDataCommandHandlerTests()
        {
            _eventBusMock = new Mock<IEventBus>();
            _someDataWriteRepositoryMock = new Mock<ISomeDataWriteRepository>();
            _businessUserRepositoryMock = new Mock<IBusinessUserRepository>();

            _businessUser = BuildBusinessUser();
            var businessUserDb = new List<BusinessUser>
            {
                _businessUser
            };
            var someDataDb = new List<SomeData>();
            var businessUserMockSet = new MockDbSet<BusinessUser>(businessUserDb);
            var someDataMockSet = new MockDbSet<SomeData>(someDataDb);
            _dbContextMock = new Mock<SomeDataContext>();
            _dbContextMock.Setup(c => c.BusinessUsers).Returns(businessUserMockSet.Object);
            _dbContextMock.Setup(c => c.SomeData).Returns(someDataMockSet.Object);
        }

        [Fact]
        public void ShouldSaveToDatabaseAndPublishCreatedEventOnHandleCreateSomeDataCommand()
        {
            var createCardCommmand = BuildCreateSomeDataCommand(new ApplicationUserId(_businessUser.IdentityId));

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

            _businessUserRepositoryMock
                .Setup(x => x.GetByApplicationUserId(createCardCommmand.ApplicationUserId, _dbContextMock.Object))
                .Returns(_businessUser);

            var commandHandler = new SomeDataCommandHandler(_eventBusMock.Object, _dbContextMock.Object, _someDataWriteRepositoryMock.Object, _businessUserRepositoryMock.Object);

            ((IRequestHandler<CreateSomeDataCommand, Unit>) commandHandler).Handle(createCardCommmand, It.IsAny<CancellationToken>());

            _eventBusMock.Verify(x => x.Publish(It.IsAny<SomeDataCreatedEvent>()), Times.Once());
            _businessUserRepositoryMock.Verify(x => x.GetByApplicationUserId(createCardCommmand.ApplicationUserId, _dbContextMock.Object), Times.Once);
            _someDataWriteRepositoryMock.Verify(x => x.Create(It.IsAny<SomeAggregate>(), _dbContextMock.Object),Times.Once);
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

            var commandHandler = new SomeDataCommandHandler(_eventBusMock.Object, _dbContextMock.Object, _someDataWriteRepositoryMock.Object, _businessUserRepositoryMock.Object);

            commandHandler.Handle(deleteSomeDataCommand, new CancellationToken());

            _eventBusMock.Verify(x => x.Publish(It.IsAny<SomeDataDeletedEvent>()), Times.Once);
            _someDataWriteRepositoryMock.Verify(x => x.Get(deleteSomeDataCommand.Id,_dbContextMock.Object));
            _someDataWriteRepositoryMock.Verify(x => x.Delete(It.IsAny<SomeAggregate>(), _dbContextMock.Object),
                Times.Once);
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

            _someDataWriteRepositoryMock.Setup(x => x.Get(updateSomeDataCommand.SomeDataId, _dbContextMock.Object))
                .Returns(BuildSomeAggregate(updateSomeDataCommand.SomeDataId));

            var commandHandler = new SomeDataCommandHandler(_eventBusMock.Object, _dbContextMock.Object, _someDataWriteRepositoryMock.Object, _businessUserRepositoryMock.Object);

            commandHandler.Handle(updateSomeDataCommand, new CancellationToken());

            _eventBusMock.Verify(x => x.Publish(It.IsAny<SomeDataUpdatedEvent>()), Times.Once);
            _someDataWriteRepositoryMock.Verify(x => x.Get(updateSomeDataCommand.SomeDataId, _dbContextMock.Object));
            _someDataWriteRepositoryMock.Verify(x => x.Save(It.IsAny<SomeAggregate>(), _dbContextMock.Object),
                Times.Once);
        }
    }
}
