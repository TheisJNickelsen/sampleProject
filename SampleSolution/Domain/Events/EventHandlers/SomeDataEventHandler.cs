using System;
using System.Threading;
using System.Threading.Tasks;
using SampleSolution.Common.Domain.Events;
using SampleSolution.Domain.Events.Events;
using Marten;

namespace SampleSolution.Domain.Events.EventHandlers
{
    public class SomeDataEventHandler : 
        IEventHandler<SomeDataCreatedEvent>, 
        IEventHandler<SomeDataDeletedEvent>,
        IEventHandler<SomeDataUpdatedEvent>
    {
        private readonly IDocumentSession _session;

        public SomeDataEventHandler(IDocumentSession session)
        {
            _session = session ?? throw new ArgumentNullException(nameof(session));
        }

        public Task Handle(SomeDataCreatedEvent @event, CancellationToken cancellationToken)
        {
            _session.Events.Append(@event.SomeDataId, @event);
            _session.SaveChanges();
            return Task.CompletedTask;
        }

        public Task Handle(SomeDataDeletedEvent @event, CancellationToken cancellationToken)
        {
            _session.Events.Append(@event.SomeDataId, @event);
            _session.SaveChanges();
            return Task.CompletedTask;
        }

        public Task Handle(SomeDataUpdatedEvent @event, CancellationToken cancellationToken)
        {
            _session.Events.Append(@event.SomeDataId, @event);
            _session.SaveChanges();
            return Task.CompletedTask;
        }
    }
}
