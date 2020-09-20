using System;

namespace HealthSup.Domain.Exception
{
    [Serializable()]
    public class InvalidNodeTypeException : System.Exception
    {
        public InvalidNodeTypeException() : base() { }

        public InvalidNodeTypeException(string message) : base(message) { }

        public InvalidNodeTypeException(string message, System.Exception inner) : base(message, inner) { }

        protected InvalidNodeTypeException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
