using System;
using System.Collections.Generic;
using System.IO;

namespace Assent.Reporters.DiffPrograms
{
    public class SemanticMergeDiffProgram : DiffProgramBase
    {
        static SemanticMergeDiffProgram()
        {
            var appData = Environment.GetEnvironmentVariable("LocalAppData");

            var paths = new List<string>
            {
               Path.Combine(appData, "semanticmerge\\semanticmergetool.exe")
            };

            DefaultSearchPaths = paths;
        }

        public static readonly IReadOnlyList<string> DefaultSearchPaths;

        public SemanticMergeDiffProgram() : base(DefaultSearchPaths)
        { }

        public SemanticMergeDiffProgram(IReadOnlyList<string> searchPaths) 
            : base(searchPaths)
        { }
    }
}
