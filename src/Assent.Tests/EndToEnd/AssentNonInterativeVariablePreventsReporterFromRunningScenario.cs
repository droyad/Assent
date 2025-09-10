using System;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace Assent.Tests.EndToEnd;

public class AssentNonInterativeVariablePreventsReporterFromRunningScenario : BaseScenario
{
    private Action? action;
    private readonly string recievedPath = $@"{GetTestDirectory()}\EndToEnd\{nameof(AssentNonInterativeVariablePreventsReporterFromRunningScenario)}.{nameof(WhenTheTestIsRun)}.received.txt";

    public void AndGivenTheAssentNonInteractiveEnvironmentVariableIsSetToTrue()
    {
        Environment.SetEnvironmentVariable("AssentNonInteractive", "TrUe");
    }

    public void WhenTheTestIsRun()
    {
        action = () => this.Assent("Foo", Configuration);
    }

    public void ThenAnAssentExceptionIsThrown()
    {
        action.Should().Throw<AssentFailedException>().WithMessage("Strings differ");
    }

    public void AndThenTheRecievedFileIsWritten()
    {
        ReaderWriter.Files.Keys.Should().Contain(recievedPath);
    }

    [TearDown]
    public void TearDown()
    {
        Environment.SetEnvironmentVariable("AssentNonInteractive", "");
    }
}