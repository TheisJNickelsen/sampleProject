using System;

namespace SampleSolution.Domain.ValueObjects
{
    public class ApplicationUserId
    {
        public string Value { get; }
        public ApplicationUserId(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Application user id cannot be null or empty.");
            }

            this.Value = value;
        }
    }
}