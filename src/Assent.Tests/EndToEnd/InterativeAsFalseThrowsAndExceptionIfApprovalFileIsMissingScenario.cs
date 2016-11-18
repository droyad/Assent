using System;
using FluentAssertions;
using NUnit.Framework;

namespace Assent.Tests.EndToEnd
{
    public class InterativeAsFalseThrowsAndExceptionIfApprovalFileIsMissingScenario : BaseScenario
    {
        private Action _action;
        private readonly string _recievedPath = $@"{GetTestDirectory()}\EndToEnd\{nameof(InterativeAsFalseThrowsAndExceptionIfApprovalFileIsMissingScenario)}.{nameof(WhenTheTestIsRun)}.received.txt";

        public void AndGivenTheConfigurationSettingIsInteractiveIsFalse()
        {
            Configuration = Configuration.SetInteractive(false);
        }

        public void WhenTheTestIsRun()
        {
            _action = () => this.Assent("Foo", Configuration);
        }

        public void ThenAnAssentExceptionIsThrown()
        {
            _action.ShouldThrow<AssentApprovedFileNotFoundException>();
        }

        public void AndThenTheRecievedFileIsNotWritten()
        {
            ReaderWriter.Files.Keys.Should().NotContain(_recievedPath);
        }

    }
}