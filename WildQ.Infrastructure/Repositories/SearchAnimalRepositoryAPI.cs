using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Interfaces;
using Domain.Entities.Models.ApiModels;
using WildQ.Infrastructure.Data;

namespace WildQ.Infrastructure.Repositories
{
    public class SearchAnimalRepositoryAPI : ISearchAnimalRepository
    {
        public async Task<List<SearchAnimal>> GetAsync(string animalName)
        {
            return await APISearchAnimal.GetAnimalsAsync(animalName);
        }
    }
}
