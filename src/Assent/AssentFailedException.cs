using System;

namespace Assent
{
    public class AssentException : Exception
    {
        public AssentException(string message) : base(message)
        {
            
        }
    }

    public class AssentFailedException : AssentException
    {
        public AssentFailedException(string message, string receivedFileName, string approvedFileName) : base(message)
        {
            ReceivedFileName = receivedFileName;
            ApprovedFileName = approvedFileName;
        }

        public string ReceivedFileName { get; set; }
        public string ApprovedFileName { get; set; }

    }

    public class AssentApprovedFileNotFoundException : Exception
    {
        public AssentApprovedFileNotFoundException(string receivedFileName, string approvedFileName) 
            : base($"The assent file '{approvedFileName}' was not found")
        {
            ReceivedFileName = receivedFileName;
            ApprovedFileName = approvedFileName;
        }

        public string ReceivedFileName { get; set; }
        public string ApprovedFileName { get; set; }

    }
}