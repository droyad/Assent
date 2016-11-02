using System.IO;

namespace Assent
{
    public class StringReaderWriter : IReaderWriter<string>
    {
        public bool Exists(string filename) => File.Exists(filename);

        public string Read(string filename) => File.ReadAllText(filename);

        public void Write(string filename, string data) => File.WriteAllText(filename, data);
        public void Delete(string filename) => File.Delete(filename);
    }
}