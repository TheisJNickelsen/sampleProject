using System;
using SampleSolution.Common.Domain.Events;

namespace SampleSolution.Domain.Events.Events
{
    public class SomeDataCreatedEvent : IEvent
    {
        public Guid SomeDataId { get; set; }
        public string FirstName { get; }
        public string MiddleName { get; }
        public string LastName { get; }
        public string Title { get; }
        public DateTime CreationDate { get; }

        public SomeDataCreatedEvent(Guid id, string firstName, string middleName, string lastName, string title, DateTime creationDate)
        {
            SomeDataId = id;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Title = title;
            CreationDate = creationDate;
        }
    }
}
