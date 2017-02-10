using System;
using Assent.Namers;
using Assent.Reporters;
using Assent.Sanitisers;

namespace Assent
{
    public interface IConfiguration<T>
    {
        INamer Namer { get; }
        IReporter Reporter { get; }
        T Extension { get; }
        IReaderWriter<T> ReaderWriter { get; }
        IComparer<T> Comparer { get; }
        bool IsInteractive { get; }

        ISanitiser<T> Sanitiser { get; }
    }

    public class Configuration : IConfiguration<string>
    {
        public Configuration()
        {
            Reporter = new DiffReporter();
            Comparer = new DefaultStringComparer(true);
            Extension = "txt";
            ReaderWriter = new StringReaderWriter();
            Namer = new DefaultNamer();
            Sanitiser = new NullSanitiser<string>();
            IsInteractive = !"true".Equals(Environment.GetEnvironmentVariable("AssentNonInteractive"), StringComparison.OrdinalIgnoreCase);
        }

        private Configuration(Configuration basedOn)
        {
            Namer = basedOn.Namer;
            Comparer = basedOn.Comparer;
            Reporter = basedOn.Reporter;
            Extension = basedOn.Extension;
            ReaderWriter = basedOn.ReaderWriter;
            Sanitiser = basedOn.Sanitiser;
            IsInteractive = basedOn.IsInteractive;
        }

        public INamer Namer { get; private set; }
        public IReporter Reporter { get; private set; }
        public string Extension { get; private set; }
        public IReaderWriter<string> ReaderWriter { get; private set; }
        public IComparer<string> Comparer { get; private set; }
        public ISanitiser<string> Sanitiser { get; private set; }

        public bool IsInteractive { get; private set; }

        public Configuration UsingNamer(INamer namer)
        {
            return new Configuration(this)
            {
                Namer = namer
            };
        }

        public Configuration UsingFixedName(string name)
        {
            return new Configuration(this)
            {
                Namer = new FixedNamer(name)
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

        public Configuration UsingSanitiser(ISanitiser<string> sanitiser)
        {
            return new Configuration(this)
            {
                Sanitiser = sanitiser
            };
        }

        public Configuration UsingSanitiser(Func<string, string> Sanitiser)
        {
            return new Configuration(this)
            {
                Sanitiser = new DelegateSanitiser<string>(Sanitiser)
            };
        }


        public Configuration SetInteractive(bool isInteractive)
        {
            return new Configuration(this)
            {
                IsInteractive = isInteractive
            };
        }
    }
}
