using System;
using FluentAssertions;
using NUnit.Framework;

namespace Assent.Tests.EndToEnd;

public class InterativeAsFalsePreventsReporterFromRunningScenario : BaseScenario
{
    private Action? action;
    private readonly string recievedPath = $@"{GetTestDirectory()}\EndToEnd\{nameof(InterativeAsFalsePreventsReporterFromRunningScenario)}.{nameof(WhenTheTestIsRun)}.received.txt";
    private readonly string approvedPath = $@"{GetTestDirectory()}\EndToEnd\{nameof(InterativeAsFalsePreventsReporterFromRunningScenario)}.{nameof(WhenTheTestIsRun)}.approved.txt";

    public void AndGivenTheConfigurationSettingIsInteractiveIsFalse()
    {
        Configuration = Configuration.SetInteractive(false);
    }

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

    [TearDown]
    public void TearDown()
    {
        Environment.SetEnvironmentVariable("AssentNonInteractive", "");
    }
}