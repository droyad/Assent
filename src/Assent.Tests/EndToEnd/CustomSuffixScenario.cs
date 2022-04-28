using System;
using System.IO;
using FluentAssertions;
using NSubstitute;

namespace Assent.Tests.EndToEnd
{
    public class CustomSuffixScenario : BaseScenario
    {
        private Action _action;
        private readonly string _receivedPath = Path.Combine(GetTestDirectory(), "EndToEnd", $"{nameof(CustomSuffixScenario)}.{nameof(WhenTheTestIsRun)}.reçu.txt");
        private readonly string _approvedPath = Path.Combine(GetTestDirectory(), "EndToEnd", $"{nameof(CustomSuffixScenario)}.{nameof(WhenTheTestIsRun)}.approuvé.txt");

        public void AndGivenCustomApprovalAndReceivedSuffixes()
        {
            Configuration = Configuration
                .UsingApprovalFileNameSuffix(".approuvé")
                .UsingReceivedFileNameSuffix(".reçu");
        }

        public void AndGivenTheApprovedFileMatches()
        {
            ReaderWriter.Files[_approvedPath] = "Foo";
        }

        public void WhenTheTestIsRun()
        {
            _action = () => this.Assent("Foo", Configuration);
        }

        public void ThenAnExceptionIsNotThrown()
        {
            _action.ShouldNotThrow();
        }

        public void AndThenTheReceivedFileIsNotWritten()
        {
            ReaderWriter.Files.Keys.Should().NotContain(_receivedPath);
        }

        public void AndThenTheReporterIsNotLaunched()
        {
            Reporter.DidNotReceiveWithAnyArgs().Report(_receivedPath, _approvedPath);
        }
    }
}
