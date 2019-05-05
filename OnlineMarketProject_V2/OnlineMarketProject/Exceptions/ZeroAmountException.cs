using System;
using System.Runtime.Serialization;

namespace OnlineMarketProject
{
    [Serializable]
    public class ZeroAmountException : ApplicationException
    {
        public ZeroAmountException()
        {
        }

        public ZeroAmountException(string message) : base(message)
        {
        }

        public ZeroAmountException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ZeroAmountException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}