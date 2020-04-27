using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace MySolution.Exceptions
{
    public class CreateAnimalWrongProcedureIdException : Exception
    {
        public CreateAnimalWrongProcedureIdException()
        {
        }

        public CreateAnimalWrongProcedureIdException(string message) : base(message)
        {
        }

        public CreateAnimalWrongProcedureIdException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CreateAnimalWrongProcedureIdException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
