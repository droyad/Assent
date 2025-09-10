namespace Assent.Namers;

/// <summary>
/// Returns the specified name without modification
/// </summary>
public class FixedNamer : INamer
{
    private readonly string name;

    public FixedNamer(string name)
    {
        this.name = name;
    }

    public string GetName(TestMetadata metadata)
    {
        return name;
    }
}