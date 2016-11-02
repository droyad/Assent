using System.IO;
using System.Runtime.CompilerServices;

namespace Assent
{
    public class DefaultNamer : INamer
    {
        public string GetName(TestMetadata metadata)
        {
            var dir = Path.GetDirectoryName(metadata.FilePath);
            return Path.Combine(dir, $"{metadata.TestFixture.GetType().Name}.{metadata.TestName}");
        }
    }
}