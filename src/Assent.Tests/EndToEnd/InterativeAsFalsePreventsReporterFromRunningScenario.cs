using System;
using FluentAssertions;
using NUnit.Framework;

namespace Assent.Tests.EndToEnd
{
    public class InterativeAsFalsePreventsReporterFromRunningScenario : BaseScenario
    {
        private Action _action;
        private readonly string _recievedPath = $@"{GetTestDirectory()}\EndToEnd\{nameof(InterativeAsFalsePreventsReporterFromRunningScenario)}.{nameof(WhenTheTestIsRun)}.received.txt";
        private readonly string _approvedPath = $@"{GetTestDirectory()}\EndToEnd\{nameof(InterativeAsFalsePreventsReporterFromRunningScenario)}.{nameof(WhenTheTestIsRun)}.approved.txt";

        public void AndGivenTheConfigurationSettingIsInteractiveIsFalse()
        {
            Configuration = Configuration.SetInteractive(false);
        }

        public void AndGivenTheApprovedFileDoesNotMatch()
        {
            ReaderWriter.Files[_approvedPath] = "Bar";
        }

        public void WhenTheTestIsRun()
        {
            _action = () => this.Assent("Foo", Configuration);
        }

        public void ThenAnAssentExceptionIsThrown()
        {
            _action.Should().Throw<AssentFailedException>().WithMessage("Strings differ at 0.\r\nReceived:Foo\r\nApproved:Bar");
        }

        public void AndThenTheRecievedFileIsWritten()
        {
            ReaderWriter.Files.Keys.Should().Contain(_recievedPath);
        }

        [TearDown]
        public void TearDown()
        {
            Environment.SetEnvironmentVariable("AssentNonInteractive", "");
        }
    }
}