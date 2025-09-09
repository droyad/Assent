using System;
using System.Collections.Generic;
using System.Linq;

namespace Assent.Reporters.DiffPrograms
{
    public class KDiff3DiffProgram : DiffProgramBase
    {
        static KDiff3DiffProgram()
        {
            DefaultSearchPaths = WindowsProgramFilePaths()
                .Select(p => $@"{p}\KDiff3\KDiff3.exe")
                .ToArray();
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