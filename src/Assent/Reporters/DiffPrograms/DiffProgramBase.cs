using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Assent.Reporters.DiffPrograms;

public abstract class DiffProgramBase : IDiffProgram
{
    protected static IReadOnlyList<string> WindowsProgramFilePaths()
    {
        var result = new[]
        {
            DirPath.GetFromEnvironmentOrNull("ProgramFiles"),
            DirPath.GetFromEnvironmentOrNull("ProgramFiles(x86)"),
            DirPath.GetFromEnvironmentOrNull("ProgramW6432"),
            DirPath.GetFromEnvironmentOrNull("LocalAppData", "Programs")
        };

        return result.Where(r => r != null).Select(r => r!).Distinct().ToArray();
    }

    public IReadOnlyList<string> SearchPaths { get; }

    protected DiffProgramBase(IReadOnlyList<string> searchPaths)
    {
        SearchPaths = searchPaths;
    }

    protected virtual string CreateProcessStartArgs(
        string receivedFile, string approvedFile)
    {
        return $"\"{receivedFile}\" \"{approvedFile}\"";
    }

    public virtual bool Launch(string receivedFile, string approvedFile)
    {
        var path = SearchPaths.FirstOrDefault(File.Exists);
        if (path == null)
            return false;

        var process = Process.Start(new ProcessStartInfo(
            path, CreateProcessStartArgs(receivedFile, approvedFile)));
        process?.WaitForExit();
        return true;
    }
}