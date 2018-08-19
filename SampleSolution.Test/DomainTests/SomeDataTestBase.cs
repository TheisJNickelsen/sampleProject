using System;
using SampleSolution.Data.Contexts.Models;
using SampleSolution.Domain.Commands.Commands;
using SampleSolution.Domain.Events.Events;
using SampleSolution.Domain.ValueObjects;

namespace SampleSolution.Test.DomainTests
{
    public abstract class SomeDataTestBase
    {
        private readonly Guid id = Guid.NewGuid();
        private readonly string firstName = "testFirstName";
        private readonly string middleName = "testMiddleName";
        private readonly string lastName = "testLastName";
        private readonly string title = "testTitle";
        private readonly DateTime creationDate = DateTime.Now;
        private readonly string color = "#123456";
        private readonly string facebookUrl = "www.facebook.com/path/file";
        private readonly string businessUserEmail = "someemail@gmail.com";

        public CreateSomeDataCommand BuildCreateSomeDataCommand()
        {
            return new CreateSomeDataCommand(id, 
                firstName, 
                middleName, 
                lastName, title, 
                new Color(color),
                creationDate, 
                new FacebookUrl(facebookUrl),
                businessUserEmail);
        }

        public DeleteSomeDataCommand BuildDeleteSomeDataCommand()
        {
            return new DeleteSomeDataCommand(id);
        }
        public UpdateSomeDataCommand BuildUpdateSomeDataCommand()
        {
            return new UpdateSomeDataCommand(id,
                firstName,
                middleName,
                lastName, title,
                new Color(color),
                creationDate,
                new FacebookUrl(facebookUrl));
        }

        public BusinessUser BuildBusinessUser()
        {
            return new BusinessUser
            {
                Id = Guid.NewGuid(),
                Gender = "SomeGender",
                Locale = "SomeLocale",
                Location = "SomeLocation",
                Identity = new ApplicationUser
                {
                    Email = businessUserEmail
                }
            };
        }

        public Data.Contexts.Models.SomeData BuildSomeDataPersistanceModel()
        {
            return new Data.Contexts.Models.SomeData
            {
                FacebookUrl = facebookUrl,
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                CreationDate = creationDate,
                Title = title,
                Id = id,
                Color = color
            };
        }

        public SomeDataCreatedEvent BuildSomeDataCreatedEvent()
        {
            return new SomeDataCreatedEvent(id, firstName, middleName, lastName, title, creationDate);
        }

        public SomeDataDeletedEvent BuildSomeDataDeletedEvent()
        {
            return new SomeDataDeletedEvent(id);
        }

        public SomeDataUpdatedEvent BuildUpdatedEvent()
        {
            return new SomeDataUpdatedEvent(id,
                firstName,
                middleName,
                lastName, title,
                new Color(color),
                creationDate,
                new FacebookUrl(facebookUrl));
        }
    }
}
