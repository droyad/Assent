using System.Security.Cryptography.X509Certificates;

namespace Assent.DiffPrograms
{
    public interface IDiffProgram
    {
        bool Launch(string receivedFile, string approvedFile);
    }
}