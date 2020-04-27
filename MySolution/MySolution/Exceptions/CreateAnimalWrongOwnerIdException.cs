using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace MySolution.Exceptions
{
    public class CreateAnimalWrongOwnerIdException : Exception
    {
        public CreateAnimalWrongOwnerIdException()
        {
        }

        public CreateAnimalWrongOwnerIdException(string message) : base(message)
        {
        }

        public CreateAnimalWrongOwnerIdException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CreateAnimalWrongOwnerIdException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
