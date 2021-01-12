using System.Collections.Generic;
using System.Linq;

namespace Assent.Reporters.DiffPrograms
{
    public class WinMergeDiffProgram : DiffProgramBase
    {
        static WinMergeDiffProgram()
        {
            DefaultSearchPaths = WindowsProgramFilePaths
                .Select(p => $@"{p}\WinMerge\WinMergeU.exe")
                .ToArray();
        }

        public static readonly IReadOnlyList<string> DefaultSearchPaths;

        public WinMergeDiffProgram() : base(DefaultSearchPaths)
        {

        }

        public WinMergeDiffProgram(IReadOnlyList<string> searchPaths)
            : base(searchPaths)
        {
        }
    }
}
