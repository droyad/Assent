using System;
using System.Collections.Generic;

namespace Assent.DiffPrograms
{
    public class KDiff3DiffProgram : DiffProgramBase
    {
        static KDiff3DiffProgram()
        {
            var programFiles = Environment.GetEnvironmentVariable("ProgramFiles");
            var x86 = Environment.GetEnvironmentVariable("ProgramFiles(x86)");

            var paths = new List<string>
            {
                $@"{programFiles}\KDiff3\KDiff3.exe",
            };

            if (!string.IsNullOrEmpty(x86))
            {
                paths.Add($@"{x86}\KDiff3\KDiff3.exe");
            }

            DefaultSearchPaths = paths;
        }

        public static readonly IReadOnlyList<string> DefaultSearchPaths;

        public KDiff3DiffProgram() : base(DefaultSearchPaths)
        {
        }

        public KDiff3DiffProgram(IReadOnlyList<string> searchPaths)
            : base(searchPaths)
        {
        }

    }
}