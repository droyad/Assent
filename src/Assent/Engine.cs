using System;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Assent;

public static class Engine<T>
{
    internal static void Execute(IConfiguration<T> configuration, TestMetadata metadata, T recieved)
    {
        recieved = configuration.Sanitiser.Sanatise(recieved);
        var name = configuration.Namer.GetName(metadata);
        var approvedFileName = $"{name}{configuration.ApprovalFileNameSuffix}.{configuration.Extension}";
        var receivedFileName = $"{name}{configuration.ReceivedFileNameSuffix}.{configuration.Extension}";

        var result = Compare(configuration, receivedFileName, approvedFileName, recieved);
        if (!result.Passed)
        {
            var reporterRan = RunReporter(configuration, recieved, receivedFileName, approvedFileName);
            if (reporterRan)
                result = Compare(configuration, receivedFileName, approvedFileName,  recieved);
        }

        if (result.Passed)
            return;

        throw new AssentFailedException(result.Error, receivedFileName, approvedFileName);
    }

    private static CompareResult Compare(IConfiguration<T> configuration, string receivedFileName, string approvedFileName, T recieved)
    {
        var approved = default(T);
        var readerWriter = configuration.ReaderWriter;
        if (readerWriter.Exists(approvedFileName))
            approved = readerWriter.Read(approvedFileName);
        else if (configuration.IsInteractive)
            readerWriter.Write(approvedFileName, approved);
        else
            throw new AssentApprovedFileNotFoundException(receivedFileName, approvedFileName);

        var result = Compare(configuration.Comparer, recieved, approved);
        return result;
    }

    private static bool RunReporter(IConfiguration<T> configuration, T recieved, string receivedFileName, string approvedFileName)
    {
        configuration.ReaderWriter.Write(receivedFileName, recieved);

        if (!configuration.IsInteractive)
            return false;

        try
        {
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