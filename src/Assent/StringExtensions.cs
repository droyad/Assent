using System.Linq.Expressions;

namespace Assent
{
    public static class StringExtensions
    {
        public static string SafeSubstring(this string str, int start, int end)
        {
            if (start < 0)
                start = 0;
            if (start > str.Length)
                start = str.Length;

            if (end > str.Length)
                end = str.Length;
            if (end < start)
                end = start;

            return str.Substring(start, end - start);
        }
    }
}