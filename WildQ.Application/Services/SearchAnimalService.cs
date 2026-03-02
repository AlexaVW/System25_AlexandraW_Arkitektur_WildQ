using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Models.ApiModels;
using WildQ.Application.Interfaces;

namespace WildQ.Application.Services
{
    public class SearchAnimalService : ISearchAnimalService
    {
        public async Task<List<SearchAnimal>> GetAnimalsAsync(string animalName)
        {
            return await Infrastructure.Data.APISearchAnimal.GetAnimalsAsync(animalName);
        }
    }
}
