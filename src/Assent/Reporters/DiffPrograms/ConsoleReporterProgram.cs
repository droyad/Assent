using System;
using System.IO;
using Assent.Reporters.DiffPrograms;
using DiffPlex;
using DiffPlex.DiffBuilder;
using DiffPlex.DiffBuilder.Model;

namespace Assent.Reporters.DiffPrograms
{
    public class ConsoleReporterProgram : IDiffProgram
    {

        public bool Launch(string receivedFile, string approvedFile)
        {
            var originalForegroundColor = Console.ForegroundColor;
            var receivedString = File.ReadAllText(receivedFile).Replace("\r\n", "\n");
            var approvedString = File.ReadAllText(approvedFile).Replace("\r\n", "\n");
            var diffBuilder = new InlineDiffBuilder(new Differ());
            var diff = diffBuilder.BuildDiffModel(approvedString, receivedString);

            foreach (var line in diff.Lines)
            {
                switch (line.Type)
                {
                    case ChangeType.Inserted:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("+ ");
                        break;
                    case ChangeType.Deleted:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("- ");
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("  ");
                        break;
                }

                Console.WriteLine(line.Text);
            }

            Console.WriteLine("The received file is different to the approved file. If this is expected, please update the approved file.");
            Console.ForegroundColor = originalForegroundColor;
            return true;
        }
    }
}