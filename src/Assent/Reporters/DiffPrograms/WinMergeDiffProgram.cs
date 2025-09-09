using System.Collections.Generic;
using System.Linq;

namespace Assent.Reporters.DiffPrograms;

public class WinMergeDiffProgram(IReadOnlyList<string> searchPaths) : DiffProgramBase(searchPaths)
{
    static WinMergeDiffProgram()
    {
        DefaultSearchPaths = WindowsProgramFilePaths()
            .Select(p => $@"{p}\WinMerge\WinMergeU.exe")
            .ToArray();
    }

    public static readonly IReadOnlyList<string> DefaultSearchPaths;

    public WinMergeDiffProgram() : this(DefaultSearchPaths)
    {

    }
}