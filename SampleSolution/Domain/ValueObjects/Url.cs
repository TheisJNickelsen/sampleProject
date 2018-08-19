using System;
using System.Collections.Generic;

namespace SampleSolution.Domain.ValueObjects
{
    public abstract class Url : ValueObject
    {
        public string Value { get; }

        protected Url(string value)
        {
            if (value == null)
            {
                Value = value;
            }
            else if (!Uri.IsWellFormedUriString(value, UriKind.RelativeOrAbsolute))
                throw new ArgumentException("Value is not a valid url: {0}", value);

            Value = value;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
