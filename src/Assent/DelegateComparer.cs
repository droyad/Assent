using System;

namespace Assent;

public class DelegateComparer<T>(Func<T, T, CompareResult> comparer) : IComparer<T>
{
    public CompareResult Compare(T recieved, T approved) => comparer(recieved, approved);
}