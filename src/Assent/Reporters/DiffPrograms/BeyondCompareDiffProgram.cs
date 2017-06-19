using System;
using System.Collections.Generic;
using System.Linq;

namespace Assent.Reporters.DiffPrograms
{
    public class BeyondCompareDiffProgram : DiffProgramBase
    {
        static BeyondCompareDiffProgram()
        {
            DefaultSearchPaths = WindowsProgramFilePaths
                .SelectMany(p =>
                    new[]
                    {
                        $@"{p}\Beyond Compare 4\BCompare.exe",
                        $@"{p}\Beyond Compare 3\BCompare.exe"
                    }
                )
                .ToArray();
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