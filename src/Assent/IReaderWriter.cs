namespace Assent
{
    public interface IReaderWriter<TData>
    {
        bool Exists(string filename);
        TData Read(string filename);
        void Write(string filename, TData data);

        void Delete(string filename);
    }
}