using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Assent.Reporters.DiffPrograms
{
    public abstract class DiffProgramBase : IDiffProgram
    {
        protected static IReadOnlyList<string> WindowsProgramFilePaths()
        {
            var result = new List<string>();
            
            if (DirPath.TryGetFromEnvironment("ProgramFiles", out var pf)) result.Add(pf);
            if (DirPath.TryGetFromEnvironment("ProgramFiles(x86)", out var pf86)) result.Add(pf86);
            if (DirPath.TryGetFromEnvironment("ProgramW6432", out var pfw64)) result.Add(pfw64);
            if (DirPath.TryGetFromEnvironment("LocalAppData", new[] { "Programs" }, out var appDataPrograms)) result.Add(appDataPrograms);

            return result.Distinct().ToList();
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
}