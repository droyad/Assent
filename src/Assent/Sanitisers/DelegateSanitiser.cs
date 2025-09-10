using System;

namespace Assent.Sanitisers;

public class DelegateSanitiser<T> : ISanitiser<T>
{
    private readonly Func<T, T> func;

    public DelegateSanitiser(Func<T,T> func)
    {
        this.func = func;
    }

    public T Sanatise(T recieved)
    {
        return func(recieved);
    }
}