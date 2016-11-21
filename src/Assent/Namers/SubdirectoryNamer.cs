using System.IO;

namespace Assent.Namers
{
    public class SubdirectoryNamer : INamer
    {
        private readonly string _subdirectory;

        public SubdirectoryNamer(string subdirectory)
        {
            _subdirectory = subdirectory;
        }

        public virtual string GetName(TestMetadata metadata)
        {
            var dir = Path.GetDirectoryName(metadata.FilePath);
            return Path.Combine(dir, _subdirectory, $"{metadata.TestFixture.GetType().Name}.{metadata.TestName}");
        }
    }
}