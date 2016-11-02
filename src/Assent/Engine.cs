using System;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Assent
{
    internal static class Engine<T>
    {

        internal static void Execute(IConfiguration<T> configuration, TestMetadata metadata, T recieved)
        {
            var name = configuration.Namer.GetName(metadata);
            var approvedFileName = name + ".approved." + configuration.Extension;
            var receivedFileName = name + ".received." + configuration.Extension;

            var result = Compare(configuration, approvedFileName, recieved);
            if (!result.Passed)
            {
                var reporterRan = RunReporter(configuration, recieved, receivedFileName, approvedFileName);
                if (reporterRan)
                    result = Compare(configuration, approvedFileName, recieved);
            }

            if (result.Passed)
                return;

            throw new AssentException(result.Error);
        }

        private static CompareResult Compare(IConfiguration<T> configuration, string approvedFileName, T recieved)
        {
            var approved = default(T);
            var readerWriter = configuration.ReaderWriter;
            if (readerWriter.Exists(approvedFileName))
                approved = readerWriter.Read(approvedFileName);
            else
                readerWriter.Write(approvedFileName, approved);

            var result = Compare(configuration.Comparer, recieved, approved);
            return result;
        }

        private static bool RunReporter(IConfiguration<T> configuration, T recieved, string receivedFileName, string approvedFileName)
        {
            var isNonInteractive = "true".Equals(Environment.GetEnvironmentVariable("AssentNonInteractive"), StringComparison.OrdinalIgnoreCase);
            if (isNonInteractive)
                return false;

            try
            {
                configuration.ReaderWriter.Write(receivedFileName, recieved);
                configuration.Reporter.Report(receivedFileName, approvedFileName);
                return true;
            }
            finally
            {
                configuration.ReaderWriter.Delete(receivedFileName);
            }
        }

        private static CompareResult Compare(IComparer<T> comparer, T recieved, T approved)
        {
            try
            {
                return comparer.Compare(recieved, approved);
            }
            catch (Exception ex)
            {
                return CompareResult.Fail(ex.ToString());
            }
        }

    }
}
