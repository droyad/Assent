using System;
using FluentAssertions;
using NUnit.Framework;

namespace Assent.Tests.EndToEnd;

public class InterativeAsFalseThrowsAndExceptionIfApprovalFileIsMissingScenario : BaseScenario
{
    private Action? action;
    private readonly string recievedPath = $@"{GetTestDirectory()}\EndToEnd\{nameof(InterativeAsFalseThrowsAndExceptionIfApprovalFileIsMissingScenario)}.{nameof(WhenTheTestIsRun)}.received.txt";

    public void AndGivenTheConfigurationSettingIsInteractiveIsFalse()
    {
        Configuration = Configuration.SetInteractive(false);
    }

    public void WhenTheTestIsRun()
    {
        action = () => this.Assent("Foo", Configuration);
    }

    public void ThenAnAssentExceptionIsThrown()
    {
        action.Should().Throw<AssentApprovedFileNotFoundException>();
    }

    public void AndThenTheRecievedFileIsNotWritten()
    {
        ReaderWriter.Files.Keys.Should().NotContain(recievedPath);
    }

}