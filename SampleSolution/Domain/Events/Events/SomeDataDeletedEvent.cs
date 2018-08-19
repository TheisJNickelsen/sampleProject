using System;
using SampleSolution.Common.Domain.Events;

namespace SampleSolution.Domain.Events.Events
{
    public class SomeDataDeletedEvent : IEvent
    {
        public Guid SomeDataId { get; set; }

        public SomeDataDeletedEvent(Guid id)
        {
            SomeDataId = id;
        }
    }
}
