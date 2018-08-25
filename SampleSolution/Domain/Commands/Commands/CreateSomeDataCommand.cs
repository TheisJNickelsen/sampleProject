using System;
using SampleSolution.Common.Domain.Commands;
using SampleSolution.Domain.ValueObjects;

namespace SampleSolution.Domain.Commands.Commands
{
    public class CreateSomeDataCommand : ICommand
    {
        public CreateSomeDataCommand(Guid id, 
            string firstName, 
            string middleName, 
            string lastName, 
            string title, 
            Color color, 
            DateTime creationDate, 
            FacebookUrl facebookUrl,
            ApplicationUserId applicationUserId)
        {
            Id = id;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Title = title;
            Color = color;
            CreationDate = creationDate;
            FacebookUrl = facebookUrl;
            ApplicationUserId = applicationUserId;
        }

        public Guid Id { get; }
        public string FirstName { get; }
        public string MiddleName { get; }
        public string LastName { get; }
        public string Title { get; }
        public Color Color { get; }
        public DateTime CreationDate { get; }
        public FacebookUrl FacebookUrl { get; }
        public ApplicationUserId ApplicationUserId { get; }

    }
}
