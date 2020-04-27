using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace MySolution.Exceptions
{
    public class AnimalsArgumentsException : Exception
    {
        public AnimalsArgumentsException()
        {
        }

        public AnimalsArgumentsException(string message) : base(message)
        {
        }

        public AnimalsArgumentsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AnimalsArgumentsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
