using System;

namespace Assent
{
    public class AssentException(string message) : Exception(message);

    public class AssentFailedException(string message, string receivedFileName, string approvedFileName)
        : AssentException(message)
    {
        public string ReceivedFileName { get; set; } = receivedFileName;
        public string ApprovedFileName { get; set; } = approvedFileName;
    }

    public class AssentApprovedFileNotFoundException(string receivedFileName, string approvedFileName) : Exception($"The assent file '{approvedFileName}' was not found")
    {
        public string ReceivedFileName { get; set; } = receivedFileName;
        public string ApprovedFileName { get; set; } = approvedFileName;
    }
}