namespace Assent
{
    public class PostfixNamer : DefaultNamer
    {
        private readonly string _postfix;

        public PostfixNamer(string postfix)
        {
            _postfix = postfix;
        }

        public override string GetName(TestMetadata metadata)
        {
            return base.GetName(metadata) + "." + _postfix;
        }
    }
}