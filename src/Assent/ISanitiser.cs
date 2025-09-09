namespace Assent;

public interface ISanitiser<T>
{
    T Sanatise(T recieved);
}