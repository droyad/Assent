using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Assent.DiffPrograms
{
    public abstract class DiffProgramBase : IDiffProgram
    {
        public IReadOnlyList<string> SearchPaths { get; }

        protected DiffProgramBase(IReadOnlyList<string> searchPaths)
        {
            SearchPaths = searchPaths;
        }


        public virtual bool Launch(string receivedFile, string approvedFile)
        {
            var path = SearchPaths.FirstOrDefault(File.Exists);
            if (path == null)
                return false;

            var process = Process.Start(new ProcessStartInfo(path, $"\"{receivedFile}\" \"{approvedFile}\""));
            process.WaitForExit();
            return true;
        }
    }
}