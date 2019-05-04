using System;
using System.Runtime.Serialization;

namespace OnlineMarketProject
{
    [Serializable]
    public class WrongChoiceException : ApplicationException
    {
        public WrongChoiceException()
        {
        }

        public WrongChoiceException(string message) : base(message)
        {
        }

        public WrongChoiceException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WrongChoiceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}