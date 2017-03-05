using System;
using System.Collections.Generic;

namespace Assent.Reporters.DiffPrograms
{
    public class P4MergeDiffProgram : DiffProgramBase
    {
        static P4MergeDiffProgram()
        {
            var programFiles = Environment.GetEnvironmentVariable("ProgramFiles");
            var x86 = Environment.GetEnvironmentVariable("ProgramFiles(x86)");

            var paths = new List<string>
            {
                $@"{programFiles}\Perforce\p4merge.exe",
            };

            if (!string.IsNullOrEmpty(x86))
            {
                paths.Add($@"{x86}\Perforce\p4merge.exe");
            }

            DefaultSearchPaths = paths;
        }

        public static readonly IReadOnlyList<string> DefaultSearchPaths;

        public P4MergeDiffProgram() : base(DefaultSearchPaths)
        {
        }

        public P4MergeDiffProgram(IReadOnlyList<string> searchPaths) : base(searchPaths)
        {
        }
    }
}