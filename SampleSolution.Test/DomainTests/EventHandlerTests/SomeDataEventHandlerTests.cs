using SampleSolution.Domain.Events.EventHandlers;
using Marten;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace SampleSolution.Test.DomainTests.EventHandlerTests
{
    public class MySampleSolutionEventHandlerTests : SomeDataTestBase
    {
        private readonly Mock<IDocumentSession> _session;
        private readonly List<Object> _events;
        private readonly MySampleSolutionEventHandler _eventHandler;

        public MySampleSolutionEventHandlerTests()
        {
            _events = new List<Object>();

            _session = new Mock<IDocumentSession>();
            _session.Setup(s => s.Events.Append(It.IsAny<Guid>(), It.IsAny<Object>())).Callback((Guid id, Object @event) =>
                {
                    _events.Add(@event);
                }
            );

            _eventHandler = new MySampleSolutionEventHandler(_session.Object);
        }

        [Fact]
        public void ShouldSaveToEventStoreOnHandleCreatedEvent()
        {
            var SomeDataCreatedEvent = BuildSomeDataCreatedEvent();

            _eventHandler.Handle(SomeDataCreatedEvent, new CancellationToken());

            _session.Verify(s => s.Events.Append(SomeDataCreatedEvent.SomeDataId, SomeDataCreatedEvent), Times.Once);
            _session.Verify(s => s.SaveChanges(), Times.Once);
            Assert.Single(_events);
        }

        [Fact]
        public void ShouldSaveToEventStoreOnHandleDeletedEvent()
        {
            var someDataDeletedEvent = BuildSomeDataDeletedEvent();

            _eventHandler.Handle(someDataDeletedEvent, new CancellationToken());

            _session.Verify(s => s.Events.Append(someDataDeletedEvent.SomeDataId, someDataDeletedEvent), Times.Once);
            _session.Verify(s => s.SaveChanges(), Times.Once);
            Assert.Single(_events);
        }


        [Fact]
        public void ShouldSaveToEventStoreOnHandleUpdatedEvent()
        {
            var someDataUpdatedEvent = BuildUpdatedEvent();

            _eventHandler.Handle(someDataUpdatedEvent, new CancellationToken());

            _session.Verify(s => s.Events.Append(someDataUpdatedEvent.SomeDataId, someDataUpdatedEvent), Times.Once);
            _session.Verify(s => s.SaveChanges(), Times.Once);
            Assert.Single(_events);
        }
    }
}
