using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Assent.Tests.Stubs;
using NSubstitute;
using NUnit.Framework;
using TestStack.BDDfy;

namespace Assent.Tests.EndToEnd;

public abstract class BaseScenario
{
    protected Configuration Configuration { get; set; } = new();

    public void GivenTestStubs()
    {
        Configuration = Configuration
            .UsingReaderWriter(ReaderWriter)
            .UsingReporter(Reporter);
    }

    protected IReporter Reporter { get; set; } = Substitute.For<IReporter>();

    protected StubReaderWriter<string> ReaderWriter { get; } = new();

    [Test]
    public void Execute()
    {
        this.BDDfy();
    }

    protected static string GetTestDirectory([CallerFilePath] string callerFilePath = "unknown")
    {
        return callerFilePath.Substring(0, callerFilePath.IndexOf("Assent.Tests")) + "Assent.Tests";
    }
}