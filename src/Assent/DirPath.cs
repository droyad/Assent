using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Assent
{
    internal static class DirPath
    {
        /// <summary>
        /// Reads the environment variable defined by `name`, and checks that it exists using Directory.Exists.
        /// If no such directory exists, will return false and `value` will be null. If it does exist, will return true and `value` will be the environment variable value.
        /// </summary>
        internal static bool TryGetFromEnvironment(string name, [MaybeNullWhen(returnValue: false)] out string value)
            => TryGetFromEnvironment(name, Array.Empty<string>(), out value);

        /// <summary>
        /// Reads the environment variable defined by `name`, appends the given subdirectories from `combineSubDirs` and checks that it exists using Directory.Exists.
        /// If no such directory exists, will return false and `value` will be null. If it does exist, will return true and `value` will be the environment variable value.
        /// </summary>
        internal static bool TryGetFromEnvironment(string name, string[] combineSubDirs, [MaybeNullWhen(returnValue: false)] out string value)
            => TryGetFromEnvironment(name, combineSubDirs, out value, Environment.GetEnvironmentVariable, Directory.Exists);

        // This method allows injected I/O functions for unit testing. Don't call it outside of tests
        internal static bool TryGetFromEnvironment(
            string name,
            string[] combineSubDirs,
            [MaybeNullWhen(returnValue: false)] out string value,
            Func<string, string> getEnvironmentVariable,
            Func<string, bool> directoryExists)
        {
            var valueFromEnv = getEnvironmentVariable(name);
            if (string.IsNullOrWhiteSpace(valueFromEnv))
            {
                value = null;
                return false;
            }

            if (combineSubDirs is { Length: > 0 })
            {
                var components = new string[combineSubDirs.Length + 1];
                components[0] = valueFromEnv;
                Array.Copy(combineSubDirs, 0, components, 1, combineSubDirs.Length);
                value = Path.Combine(components);
            }
            else
            {
                value = valueFromEnv;
            }

            if (directoryExists(value)) return true;

            value = null;
            return false;
        }
    }
}