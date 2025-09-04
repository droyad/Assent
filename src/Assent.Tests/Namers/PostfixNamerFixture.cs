using Assent.Namers;
using FluentAssertions;
using NUnit.Framework;

namespace Assent.Tests.Namers
{
    public class PostfixNamerFixture
    {
        [Test, Platform("Win")]
        public void NameIsCorrectForWindowsPath()
        {
            new PostfixNamer("foo")
                .GetName(new TestMetadata(this, "MyTest", @"c:\temp\source.cs"))
                .Should()
                .Be(@"c:\temp\PostfixNamerFixture.MyTest.foo");
        }

        [Test, Platform(Exclude = "Win")]
        public void NameIsCorrectForUnixPath()
        {
            new PostfixNamer("foo")
                .GetName(new TestMetadata(this, "MyTest", "/tmp/source.cs"))
                .Should()
                .Be("/tmp/PostfixNamerFixture.MyTest.foo");
        }
    }
}