using Assent.Namers;
using FluentAssertions;
using NUnit.Framework;

namespace Assent.Tests.Namers
{
    public class DefaultNamerFixture
    {
        [Test]
        public void NameIsCorrect()
        {
            new DefaultNamer()
                .GetName(new TestMetadata(this, "MyTest", @"c:\temp\source.cs"))
                .Should()
                .Be(@"c:\temp\DefaultNamerFixture.MyTest");
        }
    }
}