using MySolution.DTOs.Requests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MySolution.Models
{
    public class Animal
    {
        [Required(ErrorMessage = "Musisz podać imię zwierzęcia")]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "Błędne imię")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Musisz podać typ zwierzęcia")]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "Błędny typ zwierzęcia")]
        [MaxLength(100)]
        public string Type { get; set; }

        [Required]
        [Range(typeof(DateTime), "1/1/1900", "31/12/9999", ErrorMessage = "Data zarejestrowania musi być pomiędzy {1} a {2}")]
        public DateTime AdmissionDate { get; set; }

        [Required(ErrorMessage = "Musisz podać identyfikator właściciela")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Błędny format identyfikatora właściciela")]
        public int IdOwner { get; set; }

        public List<CreateAnimalRequestProcedure> Procedures { get; set; }
    }
}
