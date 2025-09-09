using System;
using System.Collections.Generic;
using System.Linq;

namespace Assent.Reporters.DiffPrograms
{
    public class P4MergeDiffProgram : DiffProgramBase
    {
        static P4MergeDiffProgram()
        {
            DefaultSearchPaths = WindowsProgramFilePaths()
                .Select(p => $@"{p}\Perforce\p4merge.exe")
                .ToArray();
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