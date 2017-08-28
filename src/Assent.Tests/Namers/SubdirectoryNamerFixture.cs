using Assent.Namers;
using FluentAssertions;
using NUnit.Framework;

namespace Assent.Tests.Namers
{
    public class SubdirectoryNamerFixture
    {
        [Test]
        public void NameIsCorrect()
        {
            new SubdirectoryNamer("SubDir")
                .GetName(new TestMetadata(this, "MyTest", @"c:\temp\source.cs"))
                .Should()
                .Be(@"c:\temp\SubDir\SubdirectoryNamerFixture.MyTest");
        }

        [Test]
        public void NameIsCorrectWithPostfix()
        {
            new SubdirectoryNamer("SubDir", "foo")
                .GetName(new TestMetadata(this, "MyTest", @"c:\temp\source.cs"))
                .Should()
                .Be(@"c:\temp\SubDir\SubdirectoryNamerFixture.MyTest.foo");
        }
    }
}