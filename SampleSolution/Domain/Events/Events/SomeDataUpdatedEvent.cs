using System;
using SampleSolution.Common.Domain.Events;
using SampleSolution.Domain.ValueObjects;

namespace SampleSolution.Domain.Events.Events
{
    public class SomeDataUpdatedEvent : IEvent
    {
        public Guid SomeDataId { get; set; }

        public SomeDataUpdatedEvent(Guid someDataId,
            string firstName,
            string middleName,
            string lastName,
            string title,
            Color color,
            DateTime creationDate,
            FacebookUrl facebookUrl)
        {
            SomeDataId = someDataId;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Title = title;
            Color = color;
            CreationDate = creationDate;
            FacebookUrl = facebookUrl;
        }
        public string FirstName { get; }
        public string MiddleName { get; }
        public string LastName { get; }
        public string Title { get; }
        public Color Color { get; }
        public DateTime CreationDate { get; }
        public FacebookUrl FacebookUrl { get; set; }
    }
}
