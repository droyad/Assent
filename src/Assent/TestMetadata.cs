namespace Assent
{
    public class TestMetadata
    {
        public TestMetadata(object testFixture, string testName, string filePath)
        {
            TestFixture = testFixture;
            TestName = testName;
            FilePath = filePath;
        }

        public object TestFixture { get; }
        public string TestName { get; }
        public string FilePath { get; }

    }
}