using System;
using FluentAssertions;
using NSubstitute;

namespace Assent.Tests.EndToEnd
{
    public class CustomComparerThatThrowsAnExceptionScenario : BaseScenario
    {
        private Action _action;

        public void AndGivenACustomComparerThatThrowsAnException()
        {
            Configuration = Configuration.UsingComparer((a, b) => { throw new Exception("Bar"); });
        }


        public void WhenTheTestIsRun()
        {
            _action = () => this.Assent("Foo", Configuration);
        }

        public void ThenAnExceptionIsThrown()
        {
            _action.ShouldThrow<AssentException>().And.Message.Should().StartWith("System.Exception: Bar");
        }
    }

    public class CustomComparerThatReturnsAResultScenario : BaseScenario
    {
        private Action _action;

        public void AndGivenACustomComparerThatThrowsAnException()
        {
            Configuration = Configuration.UsingComparer((a, b) => CompareResult.Fail("Bar"));
        }


        public void WhenTheTestIsRun()
        {
            _action = () => this.Assent("Foo", Configuration);
        }

        public void ThenAnExceptionIsThrown()
        {
            _action.ShouldThrow<AssentException>().WithMessage("Bar");
        }
    }

    public class CustomComparerWithFluentAssertionsScenario : BaseScenario
    {
        private Action _action;

        public void AndGivenACustomComparerThatThrowsAnException()
        {
            Configuration = Configuration.UsingComparer((r, a) => r.Should().Be(a));
        }


        public void WhenTheTestIsRun()
        {
            _action = () => this.Assent("Foo", Configuration);
        }

        public void ThenAnExceptionIsThrown()
        {
            _action.ShouldThrow<AssentException>().And.Message.Should().StartWith("NUnit.Framework.AssertionException: Expected string to be <null>, but found \"Foo\"");
        }
    }
}