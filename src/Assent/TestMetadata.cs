namespace Assent;

public class TestMetadata(object testFixture, string testName, string filePath)
{
    public object TestFixture { get; } = testFixture;
    public string TestName { get; } = testName;
    public string FilePath { get; } = filePath;
}