using FluentAssertions;
using NUnit.Framework;

namespace Assent.Tests
{
    public class DefaultStringComparerTests
    {
        [Test]
        public void LineEndingDifferencesResultInAFail()
        {
            new DefaultStringComparer(false)
                .Compare("A\r\nb", "A\nb")
                .Passed
                .Should()
                .BeFalse();
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
    }
}