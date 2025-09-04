using Assent.Namers;
using FluentAssertions;
using NUnit.Framework;

namespace Assent.Tests.Namers
{
    public class DefaultNamerFixture
    {
        [Test, Platform("Win")]
        public void NameIsCorrectForWindowsPath()
        {
            new DefaultNamer()
                .GetName(new TestMetadata(this, "MyTest", @"c:\temp\source.cs"))
                .Should()
                .Be(@"c:\temp\DefaultNamerFixture.MyTest");
        }

        [Test, Platform(Exclude = "Win")]
        public void NameIsCorrectForUnixPath()
        {
            new DefaultNamer()
                .GetName(new TestMetadata(this, "MyTest", "/tmp/source.cs"))
                .Should()
                .Be(@"/tmp/DefaultNamerFixture.MyTest");
        }
    }
}