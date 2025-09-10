namespace Assent.Namers;

public class PostfixNamer(string postfix) : DefaultNamer
{
    public override string GetName(TestMetadata metadata)
    {
        return base.GetName(metadata) + "." + postfix;
    }
}