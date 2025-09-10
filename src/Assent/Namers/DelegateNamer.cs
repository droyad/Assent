using System;
using System.Collections.Generic;
using System.Text;

namespace Assent.Namers;

public class DelegateNamer : INamer
{
    readonly Func<TestMetadata, string> f;

    public DelegateNamer(Func<TestMetadata, string> f)
    {
        this.f = f;
    }

    public string GetName(TestMetadata metadata)
    {
        return f(metadata);
    }
}