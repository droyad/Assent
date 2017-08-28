using Assent.Namers;
using FluentAssertions;
using NUnit.Framework;

namespace Assent.Tests.Namers
{
    public class PostfixNamerFixture
    {
        [Test]
        public void NameIsCorrect()
        {
            new PostfixNamer("foo")
                .GetName(new TestMetadata(this, "MyTest", @"c:\temp\source.cs"))
                .Should()
                .Be(@"c:\temp\PostfixNamerFixture.MyTest.foo");
        }
    }
}