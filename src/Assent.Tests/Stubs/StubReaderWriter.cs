using System;
using System.Collections.Generic;
using System.IO;

namespace Assent.Tests.Stubs;

public class StubReaderWriter<T> : IReaderWriter<T>
{
    public Dictionary<string, T> Files { get; } = new Dictionary<string, T>(StringComparer.OrdinalIgnoreCase);
    public HashSet<string> Deleted { get; } = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

    public bool Exists(string filename)
    {
        return Files.ContainsKey(filename);
    }

    public T Read(string filename)
    {
        if (Deleted.Contains(filename))
            throw new FileNotFoundException();

        return Files[filename];
    }

    public void Write(string filename, T data)
    {
        Deleted.Remove(filename);
        Files[filename] = data;
    }

    public void Delete(string filename)
    {
        Deleted.Add(filename);
    }
}