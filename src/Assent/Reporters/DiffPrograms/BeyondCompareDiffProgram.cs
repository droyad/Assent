using System;
using System.Collections.Generic;
using System.Linq;

namespace Assent.Reporters.DiffPrograms
{
    public class BeyondCompareDiffProgram : DiffProgramBase
    {
        static BeyondCompareDiffProgram()
        {
            var paths = new List<string>();
            if (DiffReporter.IsWindows)
            {
                paths.AddRange(WindowsProgramFilePaths()
                    .SelectMany(p =>
                        new[]
                        {
                            $@"{p}\Beyond Compare 5\BComp.exe",
                            $@"{p}\Beyond Compare 4\BComp.com",
                            $@"{p}\Beyond Compare 3\BComp.com"
                        })
                    .ToArray());
            }
            else
            {
                paths.Add("/usr/bin/bcomp");
                paths.Add("/usr/local/bin/bcomp");
                paths.Add("/opt/homebrew/bin/bcomp");
            }
            DefaultSearchPaths = paths;
        }

        public static readonly IReadOnlyList<string> DefaultSearchPaths;


        public BeyondCompareDiffProgram() : base(DefaultSearchPaths)
        {
        }

        public BeyondCompareDiffProgram(IReadOnlyList<string> searchPaths)
            : base(searchPaths)
        {
        }

        protected override string CreateProcessStartArgs(string receivedFile, string approvedFile)
        {
            var defaultArgs = base.CreateProcessStartArgs(receivedFile, approvedFile);
            var argChar = DiffReporter.IsWindows ? "/" : "-";
            return $"{defaultArgs} {argChar}solo";
        }
    }
}
