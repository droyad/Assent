using System;

namespace Assent
{
    public class DefaultStringComparer : IComparer<string>
    {
        private readonly bool _normaliseLineEndings;

        public DefaultStringComparer(bool normaliseLineEndings)
        {
            _normaliseLineEndings = normaliseLineEndings;
        }

        public CompareResult Compare(string received, string approved)
        {
            received = received ?? "";
            approved = approved ?? "";

            if (_normaliseLineEndings)
            {
                received = received.Replace("\r\n", "\n");
                approved = approved.Replace("\r\n", "\n");
            }

            if (received == approved)
                return CompareResult.Pass();

            var length = Math.Min(approved.Length, received.Length);
            for (var x = 0; x < length; x++)
                if (approved[x] != received[x])
                    return CompareResult.Fail($"Strings differ at {x}.{Environment.NewLine}Received:{received.SafeSubstring(x - 20, x + 20)}{Environment.NewLine}Approved:{approved.SafeSubstring(x - 20, x + 20)}");

            if (received.Length != approved.Length)
                CompareResult.Fail(
                    $"Recieved string length ({received.Length}) is different to approved string length ({approved.Length})");

            return CompareResult.Fail("Strings differ");
        }
    }
}