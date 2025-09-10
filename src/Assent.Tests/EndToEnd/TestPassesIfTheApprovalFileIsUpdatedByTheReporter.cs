using System;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace Assent.Tests.EndToEnd;

public class DelegateReporterScenario : BaseScenario
{
    private readonly string recievedPath =
        $@"{GetTestDirectory()}\EndToEnd\{nameof(DelegateReporterScenario)}.{nameof(WhenTheTestIsRun)}.received.txt";

    private readonly string approvedPath =
        $@"{GetTestDirectory()}\EndToEnd\{nameof(DelegateReporterScenario)}.{nameof(WhenTheTestIsRun)}.approved.txt";

    private string? recieved;
    private string? approved;


    public void AndGivenADelegateReporter()
    {
        Configuration = Configuration.UsingReporter((r, a) =>
        {
            recieved = r;
            approved = a;
        });
    }

    public void AndGivenTheApprovedFileDoesNotMatch()
    {
        ReaderWriter.Files[approvedPath] = "Bar";
    }

    public void WhenTheTestIsRun()
    {
        try
        {
            this.Assent("Foo", Configuration);
            Assert.Fail();
        }
        catch (Exception)
        {
        }
    }

    public void ThenTheDelegateRecievedParameterIsTheRecievedPath()
    {
        recieved.Should().Be(recievedPath);
    }

    public void ThenTheDelegateApprovedParameterIsTheApprovedPath()
    {
        approved.Should().Be(approvedPath);
    }
}