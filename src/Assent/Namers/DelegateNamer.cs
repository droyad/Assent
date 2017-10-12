using System;
using System.Collections.Generic;
using System.Text;

namespace Assent.Namers
{
    public class DelegateNamer : INamer
    {
        readonly Func<TestMetadata, string> _f;

        public DelegateNamer(Func<TestMetadata, string> f)
        {
            _f = f;
        }

        public string GetName(TestMetadata metadata)
        {
            return _f(metadata);
        }
    }
}
