using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SampleSolution.Domain.ValueObjects
{
    public class Color : ValueObject
    {
        public string Value { get; }

        public Color(string value)
        {
            var rgx = new Regex("^#(?:[0-9a-fA-F]{3}){1,2}$");
            if (!rgx.IsMatch(value))
                throw new ArgumentException("Value is not a color hex: {0}", value);

            Value = value;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
