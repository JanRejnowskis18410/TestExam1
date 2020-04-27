using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySolution.DTOs.Responses;
using MySolution.Exceptions;
using MySolution.Models;
using MySolution.Services;

namespace MySolution.Controllers
{
    [Route("api/animals")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly IDbService _service;

        public AnimalsController(IDbService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAnimals(string sortBy)
        {
            List<GetAnimalsResponse> result;
            if(!string.IsNullOrEmpty(sortBy))
            {
                var parameters = sortBy.Split(" ");
                if(parameters.Length != 2)
                {
                    throw new AnimalsArgumentsException("Niepoprawna liczba parametrów");
                } else if ((!parameters[1].ToUpper().Equals("ASC")) && (!parameters[1].ToUpper().Equals("DESC"))){
                    throw new AnimalsSortParamException("Niepoprawny parametr sortowania. Oczekiwany format: kolumna [ASC/DESC]");
                } else
                {
                    result = _service.getAnimals(parameters);
                }
            } else
            {
                result = _service.getAnimals();
            }
            return Ok(result);
        }

        [HttpPost("create")]
        public IActionResult CreateAnimal(Animal animal)
        {
            var result = _service.CreateAnimal(animal);
            return Created("api/animals/create", result);
        }
    }
}