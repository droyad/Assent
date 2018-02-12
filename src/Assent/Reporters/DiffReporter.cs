using System;
using System.Collections.Generic;
using Assent.Reporters.DiffPrograms;

namespace Assent.Reporters
{
    public class DiffReporter : IReporter
    {
#if NET45
        internal static readonly bool IsWindows = Environment.OSVersion.Platform == PlatformID.Win32NT;
#else
        internal static readonly bool IsWindows = System.Runtime.InteropServices.RuntimeInformation
            .IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows);
#endif

        static DiffReporter()
        {
            DefaultDiffPrograms = IsWindows
                ? new IDiffProgram[]
                {
                    new BeyondCompareDiffProgram(),
                    new KDiff3DiffProgram(),
                    new XdiffDiffProgram()
                }
                : new IDiffProgram[]
                {
                    new VsCodeDiffProgram(),
                };

        }

        public static readonly IReadOnlyList<IDiffProgram> DefaultDiffPrograms;

        private readonly IReadOnlyList<IDiffProgram> _diffPrograms;
        private readonly ConsoleReporter _consoleReporter;

        public DiffReporter() : this(DefaultDiffPrograms)
        {
            _consoleReporter = new ConsoleReporter();
        }

        public DiffReporter(IReadOnlyList<IDiffProgram> diffPrograms)
        {
            _diffPrograms = diffPrograms;
        }

        public void Report(string receivedFile, string approvedFile)
        {
            foreach (var program in _diffPrograms)
                if (program.Launch(receivedFile, approvedFile))
                    return;

            _consoleReporter.Report(receivedFile,approvedFile);
        }
    }
}