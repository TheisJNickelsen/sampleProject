using System;
using SampleSolution.Domain.ValueObjects;
using Xunit;

namespace SampleSolution.Test.DomainTests.ValueObjectTests
{
    public class SomeUrl : Url
    {
        public SomeUrl(string value) : base(value)
        {
        }
    }

    public class UrlTests
    {
        [Fact]
        public void ShouldThrowArgumentExceptionOnBadUrlFormat()
        {
            Assert.Throws<ArgumentException>(() => new SomeUrl("Hello World!"));
            Assert.Throws<ArgumentException>(() => new SomeUrl("http://www.contoso.com/path???/file name"));
            Assert.Throws<ArgumentException>(() => new SomeUrl("c:\\directory\filename"));
            Assert.Throws<ArgumentException>(() => new SomeUrl("file://c:/directory/filename"));
            Assert.Throws<ArgumentException>(() => new SomeUrl("http:\\host/path/file"));
            Assert.Throws<ArgumentException>(() => new SomeUrl("2013.05.29_14:33:41"));
        }

        [Fact]
        public void ShouldSucceedOnAbsoluteOrRelativeOrNullUrlFormat()
        {
            var test1 = new SomeUrl("www.contoso.com/path/file");
            var test2 = new SomeUrl("https://www.contoso.com/path/file");
            var test3 = new SomeUrl("/path/file");
            var test4 = new SomeUrl("");
            var test5 = new SomeUrl(null);
        }
    }
}
