using System;
using FluentAssertions;
using NSubstitute;

namespace Assent.Tests.EndToEnd;

public class NoApprovedFileScenario : BaseScenario
{
    private Action _action;
    private readonly string _recievedPath = $@"{GetTestDirectory()}\EndToEnd\{nameof(NoApprovedFileScenario)}.{nameof(WhenTheTestIsRun)}.received.txt";
    private readonly string _approvedPath = $@"{GetTestDirectory()}\EndToEnd\{nameof(NoApprovedFileScenario)}.{nameof(WhenTheTestIsRun)}.approved.txt";

    public void WhenTheTestIsRun()
    {
        _action = () => this.Assent("Foo", Configuration);
    }

    public void ThenAnAssentExceptionIsThrown()
    {
        _action.Should().Throw<AssentFailedException>().WithMessage("Strings differ");
    }

    public void AndThenTheRecievedFileIsWritten()
    {
        var testDirectory = GetTestDirectory();
        ReaderWriter.Files.Keys.Should().Contain(_recievedPath);
    }

    public void AndThenAnApprovedFileIsCreated()
    {
        ReaderWriter.Files.Keys.Should().Contain(_approvedPath);
    }

    public void AndThenTheApprovedFileIsBlank()
    {
        ReaderWriter.Files[_approvedPath].Should().Be(null);
    }

    public void AndThenTheReporterIsLaunched()
    {
        var testDirectory = GetTestDirectory();
        Reporter.Received().Report(_recievedPath, _approvedPath);
    }
    public void AndThenTheRecievedFileWasCleanedUp()
    {
        ReaderWriter.Deleted.Should().Contain(_recievedPath);
    }
}