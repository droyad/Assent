using System;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace Assent.Tests.EndToEnd;

public class DelegateReporterScenario : BaseScenario
{
    private readonly string _recievedPath =
        $@"{GetTestDirectory()}\EndToEnd\{nameof(DelegateReporterScenario)}.{nameof(WhenTheTestIsRun)}.received.txt";

    private readonly string _approvedPath =
        $@"{GetTestDirectory()}\EndToEnd\{nameof(DelegateReporterScenario)}.{nameof(WhenTheTestIsRun)}.approved.txt";

    private string? _recieved;
    private string? _approved;


    public void AndGivenADelegateReporter()
    {
        Configuration = Configuration.UsingReporter((r, a) =>
        {
            _recieved = r;
            _approved = a;
        });
    }

    public void AndGivenTheApprovedFileDoesNotMatch()
    {
        ReaderWriter.Files[_approvedPath] = "Bar";
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
        _recieved.Should().Be(_recievedPath);
    }

    public void ThenTheDelegateApprovedParameterIsTheApprovedPath()
    {
        _approved.Should().Be(_approvedPath);
    }
}