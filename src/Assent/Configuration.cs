using System;
using Assent.Namers;
using Assent.Reporters;

namespace Assent
{
    public interface IConfiguration<T>
    {
        INamer Namer { get; }
        IReporter Reporter { get; }
        T Extension { get; }
        IReaderWriter<T> ReaderWriter { get; }
        IComparer<T> Comparer { get; }
    }

    public class Configuration : IConfiguration<string>
    {
        public Configuration()
        {
            Reporter = new DiffReporter();
            Comparer = new DefaultStringComparer();
            Extension = "txt";
            ReaderWriter = new StringReaderWriter();
            Namer = new DefaultNamer();
        }

        private Configuration(Configuration basedOn)
        {
            Namer = basedOn.Namer;
            Comparer = basedOn.Comparer;
            Reporter = basedOn.Reporter;
            Extension = basedOn.Extension;
            ReaderWriter = basedOn.ReaderWriter;
        }

        public INamer Namer { get; private set; }
        public IReporter Reporter { get; private set; }
        public string Extension { get; private set; }
        public IReaderWriter<string> ReaderWriter { get; private set; }
        public IComparer<string> Comparer { get; private set; }

        public Configuration UsingNamer(INamer namer)
        {
            return new Configuration(this)
            {
                Namer = namer
            };
        }

        public Configuration UsingReporter(IReporter reporter)
        {
            return new Configuration(this)
            {
                Reporter = reporter
            };
        }

        /// <summary>
        /// Executes the supplied delegate as the reporter. The first parameter is the received name, and the second is the approved filename.
        /// </summary>
        /// <param name="action">(string receivedFile, string approvedFile) => void</param>
        /// <returns>A new configuration instance</returns>
        public Configuration UsingReporter(Action<string, string> action)
        {
            return new Configuration(this)
            {
                Reporter = new DelegateReporter(action)
            };
        }

        public Configuration UsingExtension(string ext)
        {
            return new Configuration(this)
            {
                Extension = ext
            };
        }

        public Configuration UsingReaderWriter(IReaderWriter<string> readerWriter)
        {
            return new Configuration(this)
            {
                ReaderWriter = readerWriter
            };
        }

        public Configuration UsingComparer(IComparer<string> comparer)
        {
            return new Configuration(this)
            {
                Comparer = comparer
            };
        }

        public Configuration UsingComparer(Func<string, string, CompareResult> comparer)
        {
            return new Configuration(this)
            {
                Comparer = new DelegateComparer<string>(comparer)
            };
        }

        public Configuration UsingComparer(Action<string, string> comparer)
        {
            return new Configuration(this)
            {
                Comparer = new DelegateComparer<string>((r, a) =>
                {
                    comparer(r, a);
                    return CompareResult.Pass();
                })
            };
        }
    }
}
