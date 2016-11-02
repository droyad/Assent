using System.Runtime.CompilerServices;
using Assent.Tests.Stubs;
using NSubstitute;
using NUnit.Framework;
using TestStack.BDDfy;

namespace Assent.Tests.EndToEnd
{
    public abstract class BaseScenario
    {
        protected Configuration Configuration { get; set; } = new Configuration();

        public void GivenTestStubs()
        {
            ReaderWriter = new StubReaderWriter<string>();
            Reporter = Substitute.For<IReporter>();

            Configuration = Configuration
                .UsingReaderWriter(ReaderWriter)
                .UsingReporter(Reporter);
        }

        protected IReporter Reporter { get; set; }

        protected StubReaderWriter<string> ReaderWriter { get; set; }

        [Test]
        public void Execute()
        {
            this.BDDfy();
        }

        protected static string GetTestDirectory([CallerFilePath] string callerFilePath = null)
        {
            return callerFilePath.Substring(0, callerFilePath.IndexOf("Assent.Tests")) + "Assent.Tests";
        }
    }
}