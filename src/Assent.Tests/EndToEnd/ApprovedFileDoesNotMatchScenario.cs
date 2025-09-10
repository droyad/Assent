using System;
using FluentAssertions;
using NSubstitute;

namespace Assent.Tests.EndToEnd;

public class ApprovedFileDoesNotMatchScenario : BaseScenario
{
    private Action? _action;
    private readonly string _recievedPath = $@"{GetTestDirectory()}\EndToEnd\{nameof(ApprovedFileDoesNotMatchScenario)}.{nameof(WhenTheTestIsRun)}.received.txt";
    private readonly string _approvedPath = $@"{GetTestDirectory()}\EndToEnd\{nameof(ApprovedFileDoesNotMatchScenario)}.{nameof(WhenTheTestIsRun)}.approved.txt";

    public void AndGivenTheApprovedFileDoesNotMatch()
    {
        ReaderWriter.Files[_approvedPath] = "Bar";
    }

    public void WhenTheTestIsRun()
    {
        _action = () => this.Assent("Foo", Configuration);
    }

    public void ThenAnAssentExceptionIsThrown()
    {
        _action.Should().Throw<AssentFailedException>().WithMessage("Strings differ at 0.\r\nReceived:Foo\r\nApproved:Bar");
    }

    public void AndThenTheRecievedFileIsWritten()
    {
        ReaderWriter.Files.Keys.Should().Contain(_recievedPath);
    }

    public void AndThenAnApprovedFileIsNotChanged()
    {
        ReaderWriter.Files[_approvedPath].Should().Be("Bar");
    }

    public void AndThenTheReporterIsLaunched()
    {
        Reporter.Received().Report(_recievedPath, _approvedPath);
    }

    public void AndThenTheRecievedFileWasCleanedUp()
    {
        ReaderWriter.Deleted.Should().Contain(_recievedPath);
    }
}