using System;
using System.IO;

namespace Assent.Namers;

public class DefaultNamer : INamer
{
    public virtual string GetName(TestMetadata metadata)
    {
        var dir = Path.GetDirectoryName(metadata.FilePath) ?? throw new Exception("Could not get directory name");
        return Path.Combine(dir, $"{metadata.TestFixture.GetType().Name}.{metadata.TestName}");
    }
}