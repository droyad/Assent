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
            var appData = Environment.GetEnvironmentVariable("LocalAppData");
            InstallPath = Path.Combine(appData, "semanticmerge\\mergetool.exe");
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
