namespace Assent.Namers;

/// <summary>
/// Returns the specified name without modification
/// </summary>
public class FixedNamer : INamer
{
    private readonly string _name;

    public FixedNamer(string name)
    {
        _name = name;
    }

    public string GetName(TestMetadata metadata)
    {
        return _name;
    }
}