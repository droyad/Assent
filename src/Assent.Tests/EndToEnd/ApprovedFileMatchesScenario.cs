using System;
using FluentAssertions;
using NSubstitute;

namespace Assent.Tests.EndToEnd;

public class ApprovedFileMatchesScenario : BaseScenario
{
    private Action _action;
    private readonly string _recievedPath = $@"{GetTestDirectory()}\EndToEnd\{nameof(ApprovedFileMatchesScenario)}.{nameof(WhenTheTestIsRun)}.received.txt";
    private readonly string _approvedPath = $@"{GetTestDirectory()}\EndToEnd\{nameof(ApprovedFileMatchesScenario)}.{nameof(WhenTheTestIsRun)}.approved.txt";

    public void AndGivenTheApprovedFileDoesNotMatch()
    {
        ReaderWriter.Files[_approvedPath] = "Foo";
    }

    public void WhenTheTestIsRun()
    {
        _action = () => this.Assent("Foo", Configuration);
    }

    public void ThenAnExceptionIsNotThrown()
    {
        _action.Should().NotThrow();
    }

    public void AndThenTheRecievedFileIsNotWritten()
    {
        ReaderWriter.Files.Keys.Should().NotContain(_recievedPath);
    }

    public void AndThenTheReporterIsLaunched()
    {
        Reporter.DidNotReceiveWithAnyArgs().Report(_recievedPath, _approvedPath);
    }

}