using System;

namespace HealthSup.Domain.Exception
{
    [Serializable()]
    public class InvalidInitialNodeTypeException: System.Exception
    {
        public InvalidInitialNodeTypeException() : base() { }

        public InvalidInitialNodeTypeException(string message) : base(message) { }

        public InvalidInitialNodeTypeException(string message, System.Exception inner) : base(message, inner) { }

        protected InvalidInitialNodeTypeException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
