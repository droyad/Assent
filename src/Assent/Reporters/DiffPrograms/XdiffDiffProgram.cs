using System;
using System.Diagnostics;
using System.IO;

namespace Assent.Reporters.DiffPrograms
{
    //Xdiff is the text-diff tool bundled with SemanticMerge (https://www.plasticscm.com/features/xmerge.html)
    public class XdiffDiffProgram : IDiffProgram
    {
        static XdiffDiffProgram()
        {
            if (DirPath.TryGetFromEnvironment("LocalAppData", new[] { "semanticmerge " }, out var semanticMergeDir))
            {
                InstallPath = Path.Combine(semanticMergeDir, "mergetool.exe");
            }
            else
            {
                InstallPath = ""; // File.Exists always returns false for empty-string
            }
        }

        public static readonly string InstallPath;

        public bool Launch(string receivedFile, string approvedFile)
        {
            if (!File.Exists(InstallPath))
                return false;

            var process = Process.Start(new ProcessStartInfo(InstallPath, $"-s=\"{approvedFile}\" -d=\"{receivedFile}\""));
            process?.WaitForExit();
            return true;
        }
    }
}
