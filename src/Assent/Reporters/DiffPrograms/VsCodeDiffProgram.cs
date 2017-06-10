using System;
using System.Collections.Generic;

namespace Assent.Reporters.DiffPrograms
{
    public class VsCodeDiffProgram : DiffProgramBase
    {
        static VsCodeDiffProgram()
        {
            var paths = new List<string>();
            if (DiffReporter.IsWindows)
            {
                var programFiles = Environment.GetEnvironmentVariable("ProgramFiles");
                var x86 = Environment.GetEnvironmentVariable("ProgramFiles(x86)");
                paths.Add($@"{programFiles}\Microsoft VS Code\Code.exe");
                if (!String.IsNullOrEmpty(x86))
                {
                    paths.Add($@"{x86}\Microsoft VS Code\Code.exe");
                }
            }
            else
            {
                paths.Add("/usr/local/bin/code");
            }
            DefaultSearchPaths = paths;
        }

        public static readonly IReadOnlyList<string> DefaultSearchPaths;


        public VsCodeDiffProgram() : base(DefaultSearchPaths)
        {
        }

        public VsCodeDiffProgram(IReadOnlyList<string> searchPaths)
            : base(searchPaths)
        {
        }

        protected override string CreateProcessStartArgs(string receivedFile, string approvedFile)
        {
            return $"--diff --wait --new-window \"{receivedFile}\" \"{approvedFile}\"";
        }
    }
}
