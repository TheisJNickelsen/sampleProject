using System;
using System.Collections.Generic;

namespace SampleSolution.Domain.ValueObjects
{
    public class IdentityId : ValueObject
    {
        public string Value { get; }

        public IdentityId(string value)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
