using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Interfaces;
using Domain.Entities.Models.ApiModels;

namespace WildQ.Infrastructure.Repositories
{
    public class SearchAnimalRepositoryAPI : ISearchAnimalRepository
    {
        public Task<List<SearchAnimal>> GetAsync(string animalName)
        {
            throw new NotImplementedException();
        }
    }
}
