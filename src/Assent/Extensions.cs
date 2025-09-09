using System.Runtime.CompilerServices;

namespace Assent;

public static class Extensions
{
    public static void Assent(
        this object testFixture, 
        string recieved, 
        Configuration configuration = null, 
        [CallerMemberName] string testName = null, 
        [CallerFilePath] string filePath = null
    )
    {
        var metadata = new TestMetadata(testFixture, testName, filePath);
        configuration = configuration ?? new Configuration();
        Engine<string>.Execute(configuration, metadata, recieved);
    }
}