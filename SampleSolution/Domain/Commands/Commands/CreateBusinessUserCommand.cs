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
            string gender)
        {
            Id = id;
            IdentityId = identityId;
            Location = location;
            Locale = locale;
            Gender = gender;
        }

        public Guid Id { get; }
        public IdentityId IdentityId { get; }
        public string Location { get; }
        public string Locale { get; }
        public string Gender { get; }
    }
}
