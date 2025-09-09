namespace Assent.Sanitisers;

public class NullSanitiser<T> : ISanitiser<T>
{
    public T Sanatise(T recieved)
    {
        return recieved;
    }
}