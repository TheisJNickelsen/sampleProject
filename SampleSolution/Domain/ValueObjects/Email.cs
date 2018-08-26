using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace SampleSolution.Domain.ValueObjects
{
    public class Email : ValueObject
    {
        public string Value { get; }

        public Email(string value)
        {
            if(!IsValidEmail(value)) throw new ArgumentException(String.Format("{0} is not a valid email address.", value));

            Value = value;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        private static bool IsValidEmail(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
