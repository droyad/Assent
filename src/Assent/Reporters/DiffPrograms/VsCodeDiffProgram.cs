using System;
using System.Collections.Generic;
using System.Linq;

namespace Assent.Reporters.DiffPrograms
{
    public class VsCodeDiffProgram : DiffProgramBase
    {
        static VsCodeDiffProgram()
        {
            var paths = new List<string>();
            if (DiffReporter.IsWindows)
            {
                paths.AddRange(WindowsProgramFilePaths
                    .Select(p => $@"{p}\Microsoft VS Code\Code.exe")
                    .ToArray());
            }
            else
            {
                paths.Add("/usr/local/bin/code");
                paths.Add("/usr/bin/code");
                paths.Add("/snap/bin/code");
                paths.Add("/Applications/Visual Studio Code.app/Contents/Resources/app/bin/code");
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
            return $"--diff --wait --new-window \"{approvedFile}\" \"{receivedFile}\"";
        }
    }
}
