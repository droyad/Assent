using System;
using System.IO;
using System.Linq;

namespace Assent;

internal static class DirPath
{
    /// <summary>
    /// Reads the environment variable defined by `name`, appends the given subdirectories from `combineSubDirs` and checks that it exists using Directory.Exists.
    /// If no such directory exists, will return null. If it does exist, will return the environment variable value.
    /// </summary>
    internal static string GetFromEnvironmentOrNull(string name, params string[] combineSubDirs)
        => GetFromEnvironmentOrNull(name, combineSubDirs, Environment.GetEnvironmentVariable, Directory.Exists);

    // This method allows injected I/O functions for unit testing. Don't call it outside of tests
    internal static string GetFromEnvironmentOrNull(
        string name,
        string[] combineSubDirs,
        Func<string, string> getEnvironmentVariable,
        Func<string, bool> directoryExists)
    {
        var pathFromEnv = getEnvironmentVariable(name);
        if (string.IsNullOrWhiteSpace(pathFromEnv))
            return null;

        var pathWithSubDirs = Path.Combine(new[] {pathFromEnv}.Concat(combineSubDirs).ToArray());

        return directoryExists(pathWithSubDirs) 
            ? pathWithSubDirs 
            : null;
    }
}