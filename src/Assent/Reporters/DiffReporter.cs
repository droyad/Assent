using System;
using System.Collections.Generic;
using Assent.Reporters.DiffPrograms;

namespace Assent.Reporters
{
    public class DiffReporter : IReporter
    {
        public static readonly IReadOnlyList<IDiffProgram> DefaultDiffPrograms = new IDiffProgram[]
        {
            new BeyondCompareDiffProgram(),
            new KDiff3DiffProgram(),
            new XdiffDiffProgram() 
        };

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
            foreach(var program in _diffPrograms)
                if (program.Launch(receivedFile, approvedFile))
                    return;

            throw new Exception("Could not find a diff program to use");
        }
    }
}