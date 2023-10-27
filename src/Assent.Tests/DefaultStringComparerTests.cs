using FluentAssertions;
using NUnit.Framework;

namespace Assent.Tests
{
    public class DefaultStringComparerTests
    {
        [Test]
        public void LineEndingDifferencesResultInAFail()
        {
            var compareResult = new DefaultStringComparer(false)
                .Compare("A\r\nb\nc", "A\nb\r\nc");
            compareResult.Passed.Should().BeFalse();
            compareResult.Error.Should().Contain("Strings differ only by line endings");
        }

        [Test]
        public void LineEndingDifferencesResultInSuccessIfNormaliseLineEndingsIsTrue()
        {
            new DefaultStringComparer(true)
                .Compare("A\r\nb\nc", "A\nb\r\nc")
                .Passed
                .Should()
                .BeTrue();
        }
        
        [Test]
        public void OneFileMissingNewLineAtEndOfFileResultInSuccessIfNormaliseLineEndingsIsTrue()
        {
            new DefaultStringComparer(true)
                .Compare("A\nb\nc\n", "A\nb\nc")
                .Passed
                .Should()
                .BeTrue();
        }
        
        [Test]
        public void OneFileMissingNewLineAtEndOfFileResultInAFail()
        {
            new DefaultStringComparer(false)
                .Compare("A\nb\nc\n", "A\nb\nc")
                .Passed
                .Should()
                .BeFalse();
        }
    }
}
