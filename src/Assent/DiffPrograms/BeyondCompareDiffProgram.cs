using System;
using System.Collections.Generic;

namespace Assent.DiffPrograms
{
    public class BeyondCompareDiffProgram : DiffProgramBase
    {
        static BeyondCompareDiffProgram()
        {
            var programFiles = Environment.GetEnvironmentVariable("ProgramFiles");
            var x86 = Environment.GetEnvironmentVariable("ProgramFiles(x86)");

            var paths = new List<string>
            {
                $@"{programFiles}\Beyond Compare 4\BCompare.exe",
                $@"{programFiles}\Beyond Compare 3\BCompare.exe"
            };

            if (!string.IsNullOrEmpty(x86))
            {
                paths.Add($@"{x86}\Beyond Compare 4\BCompare.exe");
                paths.Add($@"{x86}\Beyond Compare 3\BCompare.exe");
            }


            DefaultSearchPaths = paths;
        }

        public static readonly IReadOnlyList<string> DefaultSearchPaths;


        public BeyondCompareDiffProgram() : base(DefaultSearchPaths)
        {
        }

        public BeyondCompareDiffProgram(IReadOnlyList<string> searchPaths)
            : base(searchPaths)
        {
        }

    }
}