using System;
using System.IO;
using DiffPlex;
using DiffPlex.DiffBuilder;
using DiffPlex.DiffBuilder.Model;

namespace Assent.Reporters
{
    public class ConsoleReporter : IReporter
    {

        public void Report(string receivedFile, string approvedFile)
        {
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

            Console.WriteLine("Received file is different than approved file");
            Console.WriteLine("Please change approved files if this is ok");
        }
    }
}