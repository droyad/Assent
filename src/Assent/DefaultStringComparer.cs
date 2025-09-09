using System;

namespace Assent;

public class DefaultStringComparer(bool normaliseLineEndings, int numberOfCharactersEitherSideToReport)
    : IComparer<string>
{
    public DefaultStringComparer(bool normaliseLineEndings) : this(normaliseLineEndings, 200)
    {
    }

    public CompareResult Compare(string received, string approved)
    {
        received = received ?? "";
        approved = approved ?? "";

        if (normaliseLineEndings)
        {
            received = received.Replace("\r\n", "\n").TrimEnd('\n');
            approved = approved.Replace("\r\n", "\n").TrimEnd('\n');
        }

        if (received == approved)
            return CompareResult.Pass();

        if (!normaliseLineEndings)
        {
            var normalisedReceived = received.Replace("\r\n", "\n").TrimEnd('\n');
            var normalisedApproved = approved.Replace("\r\n", "\n").TrimEnd('\n');
            if (normalisedReceived == normalisedApproved)
                return CompareResult.Fail($"Strings differ only by line endings. Enable 'NormaliseLineEndings' to ignore line ending differences.");
        }
            
        var length = Math.Min(approved.Length, received.Length);
        for (var x = 0; x < length; x++)
            if (approved[x] != received[x])
                return CompareResult.Fail($"Strings differ at {x}.{Environment.NewLine}Received:{received.SafeSubstring(x - numberOfCharactersEitherSideToReport, x + numberOfCharactersEitherSideToReport)}{Environment.NewLine}Approved:{approved.SafeSubstring(x - numberOfCharactersEitherSideToReport, x + numberOfCharactersEitherSideToReport)}");

        if (received.Length != approved.Length)
            CompareResult.Fail(
                $"Received string length ({received.Length}) is different to approved string length ({approved.Length})");

        return CompareResult.Fail("Strings differ");
    }
}