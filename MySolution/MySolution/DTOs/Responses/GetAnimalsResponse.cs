using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySolution.DTOs.Responses
{
    public class GetAnimalsResponse
    {
        public string Name { get; set; }
        public string AnimalType { get; set; }
        public DateTime DateOfAdmission { get; set; }
        public string LastNameOfOwner { get; set; }
    }
}
