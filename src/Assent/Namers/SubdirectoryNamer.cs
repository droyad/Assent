using System.IO;

namespace Assent.Namers
{
    /// <summary>
    /// Namer that combines a subdirectory, class name and method name. 
    /// An optional postfix can be specified.
    /// eg filename: `subdirectory/class.method.postfix`
    /// </summary>
    public class SubdirectoryNamer : INamer
    {
        private readonly string _subdirectory;
        private readonly string _postfix;

        /// <summary>
        /// Creates a new SubdirectoryNamer
        /// </summary>
        /// <param name="subdirectory">The subdirectory relative to the test directory</param>
        public SubdirectoryNamer(string subdirectory)
        {
            _subdirectory = subdirectory;
        }

        /// <summary>
        /// Creates a new SubdirectoryNamer
        /// </summary>
        /// <param name="subdirectory">The subdirectory relative to the test directory</param>
        /// <param name="postfix">The postfix to apply to the end of the file name, this results in `subdirectory/class.method.postfix`</param>
        public SubdirectoryNamer(string subdirectory, string postfix)
        {
            _subdirectory = subdirectory;
            _postfix = postfix;
        }

        public virtual string GetName(TestMetadata metadata)
        {
            var dir = Path.GetDirectoryName(metadata.FilePath);
            var filename = $"{metadata.TestFixture.GetType().Name}.{metadata.TestName}";
            if (_postfix != null)
                filename += "." + _postfix;

            return Path.Combine(dir, _subdirectory, filename);
        }
    }
}