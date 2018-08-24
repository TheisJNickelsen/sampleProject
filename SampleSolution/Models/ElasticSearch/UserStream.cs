using System;
using SampleSolution.Domain.ValueObjects;

namespace SampleSolution.Models.ElasticSearch
{
    public class UserStream
    {
        public UserStream(Guid userId, Email email, string firstName, string middleName, string lastName, string imagePath)
        {
            UserId = userId;
            Email = email;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            ImagePath = imagePath;
        }

        public Guid UserId { get; }
        public Email Email { get; }
        public string FirstName { get; }
        public string MiddleName { get; }
        public string LastName { get; }
        public string ImagePath { get; }

        public string FullName => FirstName + ' ' + MiddleName + ' ' + LastName;
    }
}
