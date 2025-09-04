using System.Collections.Generic;
using System.Linq;

namespace Assent.Reporters.DiffPrograms;

public class MeldDiffProgram : DiffProgramBase
{
    static readonly IReadOnlyList<string> DefaultSearchPaths;

    static MeldDiffProgram()
    {
        var paths = new List<string>();
        if (DiffReporter.IsWindows)
        {
            paths.AddRange(WindowsProgramFilePaths()
                .Select(p => $@"{p}\Meld\Meld.exe")
                .ToArray());
        }
        else
        {
            paths.Add("/usr/bin/meld");
            paths.Add("/usr/local/bin/meld");
            paths.Add("/snap/bin/meld");
            paths.Add("/opt/homebrew/bin/meld");
        }
        DefaultSearchPaths = paths;
    }

    public MeldDiffProgram()
        : base(DefaultSearchPaths) { }

    protected override string CreateProcessStartArgs(string receivedFile, string approvedFile) => "\"" + receivedFile + "\" \"" + approvedFile + "\"";
}

