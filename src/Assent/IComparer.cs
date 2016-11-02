namespace Assent
{
    public interface IComparer<in TData>
    {
        CompareResult Compare(TData recieved, TData approved);
    }
}