using System;
using SampleSolution.Common.Domain.Aggregates;
using SampleSolution.Data.Contexts.Models;
using SampleSolution.Domain.ValueObjects;

namespace SampleSolution.Domain.Aggregates
{
    public class SomeAggregate : AggregateRoot
    {
        //For Automapper
        protected SomeAggregate()
        {
        }
        public string FirstName { get; protected set; }
        public string MiddleName { get; protected set; }
        public string LastName { get; protected set; }
        public string Title { get; protected set; }
        public Color Color { get; protected set; }
        public DateTime CreationDate { get; protected set; }
        public FacebookUrl FacebookUrl { get; protected set; }
        public Guid BusinessUserId { get; protected set; }

        public static SomeAggregate Create(Guid id, 
            string firstName, 
            string middleName, 
            string lastName, 
            string title, 
            Color color, 
            DateTime creationDate,
            FacebookUrl facebookUrl,
            Guid businessUserId)
        {
            return new SomeAggregate
            {
                Id = id,
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                Title = title,
                Color = color,
                CreationDate = creationDate,
                FacebookUrl = facebookUrl,
                BusinessUserId = businessUserId
            };
        }
    }
}
