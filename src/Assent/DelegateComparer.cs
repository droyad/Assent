using System;

namespace Assent
{
    public class DelegateComparer<T> : IComparer<T>
    {
        readonly Func<T, T, CompareResult> _comparer;

        public DelegateComparer(Func<T, T, CompareResult> comparer)
        {
            _comparer = comparer;
        }

        public CompareResult Compare(T recieved, T approved) => _comparer(recieved, approved);
    }
}