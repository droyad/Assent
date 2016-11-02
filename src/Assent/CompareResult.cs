namespace Assent
{
    public class CompareResult
    {
        private CompareResult()
        {
        }

        public static CompareResult Pass()
        {
            return new CompareResult()
            {
                Passed = true
            };
        }

        public static CompareResult Fail(string error)
        {
            return new CompareResult()
            {
                Error = error
            };
        }

        public string Error { get; private set; }

        public bool Passed { get; private set; }
    }
}