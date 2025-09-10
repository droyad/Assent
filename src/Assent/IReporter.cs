namespace Assent;

public interface IReporter
{
    void Report(string receivedFile, string approvedFile);
}