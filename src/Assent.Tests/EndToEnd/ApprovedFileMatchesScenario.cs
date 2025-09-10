using System;
using FluentAssertions;
using NSubstitute;

namespace Assent.Tests.EndToEnd;

public class ApprovedFileMatchesScenario : BaseScenario
{
    private Action? action;
    private readonly string recievedPath = $@"{GetTestDirectory()}\EndToEnd\{nameof(ApprovedFileMatchesScenario)}.{nameof(WhenTheTestIsRun)}.received.txt";
    private readonly string approvedPath = $@"{GetTestDirectory()}\EndToEnd\{nameof(ApprovedFileMatchesScenario)}.{nameof(WhenTheTestIsRun)}.approved.txt";

    public void AndGivenTheApprovedFileDoesNotMatch()
    {
        ReaderWriter.Files[approvedPath] = "Foo";
    }

    public void WhenTheTestIsRun()
    {
        action = () => this.Assent("Foo", Configuration);
    }

    public void ThenAnExceptionIsNotThrown()
    {
        action.Should().NotThrow();
    }

    public void AndThenTheRecievedFileIsNotWritten()
    {
        ReaderWriter.Files.Keys.Should().NotContain(recievedPath);
    }

    public void AndThenTheReporterIsLaunched()
    {
        Reporter.DidNotReceiveWithAnyArgs().Report(recievedPath, approvedPath);
    }

}