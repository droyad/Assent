using System;

namespace Assent.Reporters
{
    public class DelegateReporter : IReporter
    {
        private readonly Action<string, string> _action;

        public DelegateReporter(Action<string, string> reporter)
        {
            _action = reporter;
        }

        public void Report(string receivedFile, string approvedFile)
        {
            _action(receivedFile, approvedFile);
        }
    }
}