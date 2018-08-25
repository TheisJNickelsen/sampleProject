using SampleSolution.Common.Domain.Aggregates;
using SampleSolution.Domain.Commands.Commands;
using SampleSolution.Domain.ValueObjects;
using System;

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
        public ApplicationUserId ApplicationUserId { get; protected set; }

        public static SomeAggregate Create(Guid id, 
            string firstName, 
            string middleName, 
            string lastName, 
            string title, 
            Color color, 
            DateTime creationDate,
            FacebookUrl facebookUrl,
            ApplicationUserId applicationUserId)
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
                ApplicationUserId = applicationUserId
            };
        }

        public static SomeAggregate Create(CreateSomeDataCommand createCommand)
        {
            return new SomeAggregate
            {
                Id = createCommand.Id,
                FirstName = createCommand.FirstName,
                MiddleName = createCommand.MiddleName,
                LastName = createCommand.LastName,
                Title = createCommand.Title,
                Color = createCommand.Color,
                CreationDate = createCommand.CreationDate,
                FacebookUrl = createCommand.FacebookUrl,
                ApplicationUserId = createCommand.ApplicationUserId
            };
        }
    }
}
