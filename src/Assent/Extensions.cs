using System.Runtime.CompilerServices;

namespace Assent;

public static class Extensions
{
    public static void Assent(
        this object testFixture, 
        string recieved, 
        Configuration? configuration = null, 
        [CallerMemberName] string testName = "unknown", 
        [CallerFilePath] string filePath = "unknown"
    )
    {
        var metadata = new TestMetadata(testFixture, testName, filePath);
        Engine<string>.Execute(configuration ?? new Configuration(), metadata, recieved);
    }
}