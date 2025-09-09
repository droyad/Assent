using Assent.Namers;
using FluentAssertions;
using NUnit.Framework;

namespace Assent.Tests.Namers;

public class SubdirectoryNamerFixture
{
    [Test, Platform("Win")]
    public void NameIsCorrectForWindowsPath()
    {
        new SubdirectoryNamer("SubDir")
            .GetName(new TestMetadata(this, "MyTest", @"c:\temp\source.cs"))
            .Should()
            .Be(@"c:\temp\SubDir\SubdirectoryNamerFixture.MyTest");
    }
        
    [Test, Platform(Exclude = "Win")]
    public void NameIsCorrectForUnixPath()
    {
        new SubdirectoryNamer("SubDir")
            .GetName(new TestMetadata(this, "MyTest", "/tmp/source.cs"))
            .Should()
            .Be("/tmp/SubDir/SubdirectoryNamerFixture.MyTest");
    }

    [Test, Platform("Win")]
    public void NameIsCorrectWithPostfixForWindowsPath()
    {
        new SubdirectoryNamer("SubDir", "foo")
            .GetName(new TestMetadata(this, "MyTest", @"c:\temp\source.cs"))
            .Should()
            .Be(@"c:\temp\SubDir\SubdirectoryNamerFixture.MyTest.foo");
    }
        
    [Test, Platform(Exclude = "Win")]
    public void NameIsCorrectWithPostfixForUnixPath()
    {
        new SubdirectoryNamer("SubDir", "foo")
            .GetName(new TestMetadata(this, "MyTest", "/tmp/source.cs"))
            .Should()
            .Be("/tmp/SubDir/SubdirectoryNamerFixture.MyTest.foo");
    }
}