using System;
using System.Runtime.Serialization;

namespace CarAdverts.Domain.Core.Persistence
{
    public class NoEntryException<TKey> : Exception where TKey : struct
    {
        public TKey Id { get; }

        public NoEntryException(string message, TKey id) : this(message, id, null)
        {
        }

        public NoEntryException(string message, TKey id, Exception innerException) : base(message, innerException)
        {
            Id = id;
        }

        protected NoEntryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
