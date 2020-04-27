using MySolution.DTOs.Responses;
using MySolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySolution.Services
{
    public interface IDbService
    {
        List<GetAnimalsResponse> getAnimals();
        List<GetAnimalsResponse> getAnimals(string[] parameters);
        int CreateAnimal(Animal animal); 
    }
}
