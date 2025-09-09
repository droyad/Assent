using System;

namespace Assent.Sanitisers;

public class DelegateSanitiser<T> : ISanitiser<T>
{
    private readonly Func<T, T> _func;

    public DelegateSanitiser(Func<T,T> func)
    {
        _func = func;
    }

    public T Sanatise(T recieved)
    {
        return _func(recieved);
    }
}