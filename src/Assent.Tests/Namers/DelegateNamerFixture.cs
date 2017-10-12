using System;
using System.Collections.Generic;
using System.Text;
using Assent.Namers;
using FluentAssertions;
using NUnit.Framework;

namespace Assent.Tests.Namers
{
    class DelegateNamerFixture
    {
        [Test]
        public void NameIsCorrect()
        {
            var metadata = new TestMetadata(this, "MyTest", @"c:\temp\source.cs");
            Func<TestMetadata, string> namer = m => m.FilePath;

            new DelegateNamer(namer)
                .GetName(metadata)
                .Should()
                .Be(namer(metadata));
        }
    }
}
