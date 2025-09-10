using System;
using FluentAssertions;
using NSubstitute;

namespace Assent.Tests.EndToEnd;

public class ApprovedFileDoesNotMatchScenario : BaseScenario
{
    private Action? action;
    private readonly string recievedPath = $@"{GetTestDirectory()}\EndToEnd\{nameof(ApprovedFileDoesNotMatchScenario)}.{nameof(WhenTheTestIsRun)}.received.txt";
    private readonly string approvedPath = $@"{GetTestDirectory()}\EndToEnd\{nameof(ApprovedFileDoesNotMatchScenario)}.{nameof(WhenTheTestIsRun)}.approved.txt";

    public void AndGivenTheApprovedFileDoesNotMatch()
    {
        ReaderWriter.Files[approvedPath] = "Bar";
    }

    public void WhenTheTestIsRun()
    {
        action = () => this.Assent("Foo", Configuration);
    }

    public void ThenAnAssentExceptionIsThrown()
    {
        action.Should().Throw<AssentFailedException>().WithMessage("Strings differ at 0.\r\nReceived:Foo\r\nApproved:Bar");
    }

    public void AndThenTheRecievedFileIsWritten()
    {
        ReaderWriter.Files.Keys.Should().Contain(recievedPath);
    }

    public void AndThenAnApprovedFileIsNotChanged()
    {
        ReaderWriter.Files[approvedPath].Should().Be("Bar");
    }

    public void AndThenTheReporterIsLaunched()
    {
        Reporter.Received().Report(recievedPath, approvedPath);
    }

    public void AndThenTheRecievedFileWasCleanedUp()
    {
        ReaderWriter.Deleted.Should().Contain(recievedPath);
    }
}