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
                    new EnvironmentVariableDiffProgram(),
                    new BeyondCompareDiffProgram(),
                    new WinMergeDiffProgram(),
                    new KDiff3DiffProgram(),
                    new XdiffDiffProgram(),
                    new P4MergeDiffProgram(),
                    new RiderDiffProgram(),
                    new VsCodeDiffProgram(),
                    new MeldDiffProgram()
                }
                : new IDiffProgram[]
                {
                    new EnvironmentVariableDiffProgram(),
                    new BeyondCompareDiffProgram(),
                    new VsCodeDiffProgram(),
                    new MeldDiffProgram()
                };

        }

        public static readonly IReadOnlyList<IDiffProgram> DefaultDiffPrograms;

        private readonly IReadOnlyList<IDiffProgram> _diffPrograms;


        public DiffReporter() : this(DefaultDiffPrograms)
        {
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

            throw new Exception("Could not find a diff program to use");
        }
    }
}
