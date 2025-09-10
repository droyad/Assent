using System;
using FluentAssertions;
using NSubstitute;

namespace Assent.Tests.EndToEnd;

public class TestPassesIfTheApprovalFileIsUpdatedByTheReporterScenario : BaseScenario
{
    private Action? action;
    private readonly string recievedPath = $@"{GetTestDirectory()}\EndToEnd\{nameof(TestPassesIfTheApprovalFileIsUpdatedByTheReporterScenario)}.{nameof(WhenTheTestIsRun)}.received.txt";
    private readonly string approvedPath = $@"{GetTestDirectory()}\EndToEnd\{nameof(TestPassesIfTheApprovalFileIsUpdatedByTheReporterScenario)}.{nameof(WhenTheTestIsRun)}.approved.txt";

    public void AndGivenTheApprovedFileDoesNotMatch()
    {
        ReaderWriter.Files[approvedPath] = "Bar";
    }

    public void WhenTheTestIsRun()
    {
        action = () => this.Assent("Foo", Configuration);
    }

    public void AndWhenTheApprovedFileIsUpdatedToMatchDuringReporting()
    {
        Reporter.When(_ => _.Report(recievedPath, approvedPath))
            .Do(c => ReaderWriter.Files[approvedPath] = "Foo");
    }

    public void ThenNoExceptionIsThrown()
    {
        action.Should().NotThrow();
    }
}