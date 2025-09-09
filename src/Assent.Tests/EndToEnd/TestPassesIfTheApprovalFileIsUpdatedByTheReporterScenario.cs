using System;
using FluentAssertions;
using NSubstitute;

namespace Assent.Tests.EndToEnd;

public class TestPassesIfTheApprovalFileIsUpdatedByTheReporterScenario : BaseScenario
{
    private Action _action;
    private readonly string _recievedPath = $@"{GetTestDirectory()}\EndToEnd\{nameof(TestPassesIfTheApprovalFileIsUpdatedByTheReporterScenario)}.{nameof(WhenTheTestIsRun)}.received.txt";
    private readonly string _approvedPath = $@"{GetTestDirectory()}\EndToEnd\{nameof(TestPassesIfTheApprovalFileIsUpdatedByTheReporterScenario)}.{nameof(WhenTheTestIsRun)}.approved.txt";

    public void AndGivenTheApprovedFileDoesNotMatch()
    {
        ReaderWriter.Files[_approvedPath] = "Bar";
    }

    public void WhenTheTestIsRun()
    {
        _action = () => this.Assent("Foo", Configuration);
    }

    public void AndWhenTheApprovedFileIsUpdatedToMatchDuringReporting()
    {
        Reporter.When(_ => _.Report(_recievedPath, _approvedPath))
            .Do(c => ReaderWriter.Files[_approvedPath] = "Foo");
    }

    public void ThenNoExceptionIsThrown()
    {
        _action.Should().NotThrow();
    }
}