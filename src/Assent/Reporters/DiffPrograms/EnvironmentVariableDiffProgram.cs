using System;
using System.Diagnostics;

namespace Assent.Reporters.DiffPrograms
{
    public class EnvironmentVariableDiffProgram : IDiffProgram
    {
        public bool Launch(string receivedFile, string approvedFile)
        {
            var diffProgram = Environment.GetEnvironmentVariable("AssentDiffProgram");

            if (string.IsNullOrWhiteSpace(diffProgram))
                return false;

            var argumentFormat = Environment.GetEnvironmentVariable("AssentDiffProgramArguments") ?? "\"{0}\" \"{1}\"";

            var args = string.Format(argumentFormat, receivedFile, approvedFile);
            
            var process = Process.Start(new ProcessStartInfo(diffProgram, args));
            process?.WaitForExit();
            return true;
        }
    }
}