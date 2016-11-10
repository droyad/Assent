using System.IO;

namespace Assent.Namers
{
    public class DefaultNamer : INamer
    {
        public virtual string GetName(TestMetadata metadata)
        {
            var dir = Path.GetDirectoryName(metadata.FilePath);
            return Path.Combine(dir, $"{metadata.TestFixture.GetType().Name}.{metadata.TestName}");
        }
    }
}