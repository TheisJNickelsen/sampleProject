using SampleSolution.Common.Domain.Events;
using System;
using SampleSolution.Domain.ValueObjects;

namespace SampleSolution.Domain.Events.Events
{
    public class UserCreatedEvent : IEvent
    {
        public UserCreatedEvent(Guid userId, Email email, string firstName, string middleName, string lastName, string location, string imagePath)
        {
            UserId = userId;
            Email = email;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Location = location;
            ImagePath = imagePath;
        }

        public Guid UserId { get; }
        public Email Email { get; }
        public string FirstName { get; }
        public string MiddleName { get; }
        public string LastName { get; }
        public string Location { get; }
        public string ImagePath { get; }
    }
}
