using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using Assent.Reporters.DiffPrograms;

namespace Assent.Reporters
{
    public class DiffReporter : IReporter
    {

        static DiffReporter()
        {

#if NET45
            var isWindows = Environment.OSVersion.Platform == PlatformID.Win32NT;
#else
            var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
#endif
            DefaultDiffPrograms = isWindows
                ? new IDiffProgram[]
                {
                    new BeyondCompareDiffProgram(),
                    new KDiff3DiffProgram(),
                    new XdiffDiffProgram()
                }
                : new IDiffProgram[0];
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