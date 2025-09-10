using System;
using System.IO;
using FluentAssertions;
using NSubstitute;

namespace Assent.Tests.EndToEnd;

public class CustomSuffixScenario : BaseScenario
{
    private Action? action;
    private readonly string receivedPath = Path.Combine(GetTestDirectory(), "EndToEnd", $"{nameof(CustomSuffixScenario)}.{nameof(WhenTheTestIsRun)}.reçu.txt");
    private readonly string approvedPath = Path.Combine(GetTestDirectory(), "EndToEnd", $"{nameof(CustomSuffixScenario)}.{nameof(WhenTheTestIsRun)}.approuvé.txt");

    public void AndGivenCustomApprovalAndReceivedSuffixes()
    {
        Configuration = Configuration
            .UsingApprovalFileNameSuffix(".approuvé")
            .UsingReceivedFileNameSuffix(".reçu");
    }

    public void AndGivenTheApprovedFileMatches()
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

    public void AndThenTheReceivedFileIsNotWritten()
    {
        ReaderWriter.Files.Keys.Should().NotContain(receivedPath);
    }

    public void AndThenTheReporterIsNotLaunched()
    {
        Reporter.DidNotReceiveWithAnyArgs().Report(receivedPath, approvedPath);
    }
}