using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace MySolution.Exceptions
{
    public class AnimalsSortParamException : Exception
    {
        public AnimalsSortParamException()
        {
        }

        public AnimalsSortParamException(string message) : base(message)
        {
        }

        public AnimalsSortParamException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AnimalsSortParamException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
