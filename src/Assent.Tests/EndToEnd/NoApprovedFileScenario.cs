using System;
using FluentAssertions;
using NSubstitute;

namespace Assent.Tests.EndToEnd;

public class NoApprovedFileScenario : BaseScenario
{
    private Action? action;
    private readonly string recievedPath = $@"{GetTestDirectory()}\EndToEnd\{nameof(NoApprovedFileScenario)}.{nameof(WhenTheTestIsRun)}.received.txt";
    private readonly string approvedPath = $@"{GetTestDirectory()}\EndToEnd\{nameof(NoApprovedFileScenario)}.{nameof(WhenTheTestIsRun)}.approved.txt";

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
        var testDirectory = GetTestDirectory();
        ReaderWriter.Files.Keys.Should().Contain(recievedPath);
    }

    public void AndThenAnApprovedFileIsCreated()
    {
        ReaderWriter.Files.Keys.Should().Contain(approvedPath);
    }

    public void AndThenTheApprovedFileIsBlank()
    {
        ReaderWriter.Files[approvedPath].Should().Be(null);
    }

    public void AndThenTheReporterIsLaunched()
    {
        var testDirectory = GetTestDirectory();
        Reporter.Received().Report(recievedPath, approvedPath);
    }
    public void AndThenTheRecievedFileWasCleanedUp()
    {
        ReaderWriter.Deleted.Should().Contain(recievedPath);
    }
}