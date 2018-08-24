using System;
using SampleSolution.Common.Domain.Commands;
using SampleSolution.Domain.ValueObjects;

namespace SampleSolution.Domain.Commands.Commands
{
    public class CreateBusinessUserCommand : ICommand
    {
        public CreateBusinessUserCommand(Guid id, 
            IdentityId identityId, 
            string location, 
            string locale, 
            string gender, 
            Email email, 
            string firstName, 
            string middelName,
            string lastName)
        {
            Id = id;
            IdentityId = identityId;
            Location = location;
            Locale = locale;
            Gender = gender;
            Email = email;
            FirstName = firstName;
            MiddelName = middelName;
            LastName = lastName;
        }

        public Guid Id { get; }
        public IdentityId IdentityId { get; }
        public string Location { get; }
        public string Locale { get; }
        public string Gender { get; }
        public Email Email { get; }
        public string FirstName { get; }
        public string MiddelName { get; }
        public string LastName { get;}
    }
}
