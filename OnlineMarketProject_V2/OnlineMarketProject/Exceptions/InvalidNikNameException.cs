using System;
using System.Runtime.Serialization;

namespace OnlineMarketProject
{
    [Serializable]
    public class InvalidNikNameException : ApplicationException
    {
        public InvalidNikNameException()
        {
        }

        public InvalidNikNameException(string message) : base(message)
        {
        }

        public InvalidNikNameException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidNikNameException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}