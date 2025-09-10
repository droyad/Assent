using System;
using FluentAssertions;
using NSubstitute;

namespace Assent.Tests.EndToEnd;

public class CustomComparerThatThrowsAnExceptionScenario : BaseScenario
{
    private Action? _action;

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
        _action.Should().Throw<AssentFailedException>().And.Message.Should().StartWith("System.Exception: Bar");
    }
}

public class CustomComparerThatReturnsAResultScenario : BaseScenario
{
    private Action? _action;

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
        _action.Should().Throw<AssentFailedException>().WithMessage("Bar");
    }
}

public class CustomComparerWithFluentAssertionsScenario : BaseScenario
{
    private Action? _action;

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
        _action.Should().Throw<AssentFailedException>().And.Message.Should().StartWith("NUnit.Framework.AssertionException: Expected r to be <null>, but found \"Foo\"");
    }
}