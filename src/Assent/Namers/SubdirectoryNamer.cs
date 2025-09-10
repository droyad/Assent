using System;
using System.IO;

namespace Assent.Namers;

/// <summary>
/// Namer that combines a subdirectory, class name and method name. 
/// An optional postfix can be specified.
/// eg filename: `subdirectory/class.method.postfix`
/// </summary>
public class SubdirectoryNamer : INamer
{
    private readonly string subdirectory;
    private readonly string? postfix;

    /// <summary>
    /// Creates a new SubdirectoryNamer
    /// </summary>
    /// <param name="subdirectory">The subdirectory relative to the test directory</param>
    public SubdirectoryNamer(string subdirectory)
    {
        this.subdirectory = subdirectory;
    }

    /// <summary>
    /// Creates a new SubdirectoryNamer
    /// </summary>
    /// <param name="subdirectory">The subdirectory relative to the test directory</param>
    /// <param name="postfix">The postfix to apply to the end of the file name, this results in `subdirectory/class.method.postfix`</param>
    public SubdirectoryNamer(string subdirectory, string postfix)
    {
        this.subdirectory = subdirectory;
        this.postfix = postfix;
    }

    public virtual string GetName(TestMetadata metadata)
    {
        var dir = Path.GetDirectoryName(metadata.FilePath) ?? throw new Exception("Could not get directory name");
        var filename = $"{metadata.TestFixture.GetType().Name}.{metadata.TestName}";
        if (postfix != null)
            filename += "." + postfix;

        return Path.Combine(dir, subdirectory, filename);
    }
}