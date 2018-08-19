using System;
using SampleSolution.Domain.ValueObjects;
using Xunit;

namespace SampleSolution.Test.DomainTests.ValueObjectTests
{
    public class ColorTests
    {
        [Fact]
        public void ShouldThrowArgumentExceptionBadColorHexOrNullOrEmptyInput()
        {
            Assert.Throws<ArgumentException>(() => new Color("Hello World!"));
            Assert.Throws<ArgumentException>(() => new Color("#1234"));
            Assert.Throws<ArgumentException>(() => new Color("#f3f3"));
            Assert.Throws<ArgumentException>(() => new Color("#f3f3f"));
            Assert.Throws<ArgumentException>(() => new Color(""));
            Assert.Throws<ArgumentNullException>(() => new Color(null));
        }

        [Fact]
        public void ShouldCreateColorOnCorrectColorHex()
        {
            var test1 = new Color("#c1d0c3");
            var test2 = new Color("#c1d");
        }
    }
}
