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
                    new XdiffDiffProgram(),
                    new P4MergeDiffProgram(), 
                    new VsCodeDiffProgram()
                }
                : new IDiffProgram[]
                {
                    new BeyondCompareDiffProgram(),
                    new VsCodeDiffProgram()
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
