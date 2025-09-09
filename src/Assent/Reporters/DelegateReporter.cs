using System;

namespace Assent.Reporters
{
    public class DelegateReporter(Action<string, string> reporter) : IReporter
    {
        public void Report(string receivedFile, string approvedFile)
        {
            reporter(receivedFile, approvedFile);
        }
    }
}