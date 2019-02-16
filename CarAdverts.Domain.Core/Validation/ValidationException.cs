using System;
using System.Runtime.Serialization;

namespace CarAdverts.Domain.Core.Validation
{
    public class ValidationException : Exception
    {
        public ValidationException(string message, string paramName) : this(message, null, paramName)
        {
        }

        public ValidationException(string message, Exception innerException, string paramName) : base(message, innerException)
        {
            ParamName = paramName;
        }

        protected ValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public string ParamName { get; }
    }
}
