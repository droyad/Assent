using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Assent.Reporters.DiffPrograms;

public class RiderDiffProgram : DiffProgramBase
{
    static readonly IReadOnlyList<string> DefaultSearchPaths;

    static RiderDiffProgram()
    {
        var riderChannelsDirectory = DirPath.GetFromEnvironmentOrNull("LocalAppData", "JetBrains", "Toolbox", "apps", "Rider");
        if (riderChannelsDirectory == null)
        {
            DefaultSearchPaths = [];
            return;
        }

        DefaultSearchPaths = Directory.GetDirectories(riderChannelsDirectory).Select(GetRiderExePathInChannel).ToList();
    }

    public RiderDiffProgram() : base(DefaultSearchPaths)
    {
    }

    protected override string CreateProcessStartArgs(string receivedFile, string approvedFile) =>
        $"\"diff\" \"{receivedFile}\" \"{approvedFile}\"";

    protected static string GetRiderExePathInChannel(string channelDirectory)
    {
        var versions = Directory.GetDirectories(channelDirectory)
            .Where(directory => Regex.IsMatch(directory, @".*[0-9]+\.[0-9]+\.[0-9]+$"))
            .Select(path => GetRiderVersion(path.Replace(channelDirectory + "\\", "")))
            .ToArray();

        if (!versions.Any())
            throw new Exception($"No rider versions found in {channelDirectory}");
        
        var newestVersion = versions.MaxBy(x => x, new RiderVersionComparer());

        return $"{channelDirectory}\\{newestVersion!.Major}.{newestVersion.Minor}.{newestVersion.Patch}\\bin\\rider64.exe";
    }

    protected static RiderVersion GetRiderVersion(string version)
    {
        var split = version.Split(".");
        if (split.Length != 3)
        {
            //Fallback to empty version
            return new RiderVersion(0, 0, 0);
        }

        return new RiderVersion(int.Parse(split[0]), int.Parse(split[1]), int.Parse(split[2]));
    }
}

public record RiderVersion(int Major, int Minor, int Patch);

public class RiderVersionComparer : System.Collections.Generic.IComparer<RiderVersion>
{
    public int Compare(RiderVersion? x, RiderVersion? y)
    {
        if (x == null)
        {
            return -1;
        }

        if (y == null)
        {
            return 1;
        }

        if (x.Major - y.Major != 0)
        {
            return x.Major - y.Major;
        }

        if (x.Minor - y.Minor != 0)
        {
            return x.Minor - y.Minor;
        }

        return x.Patch - y.Patch;
    }
}