using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Interfaces
{
    public interface ISearchAnimalRepository
    {
        Task<List<Models.ApiModels.SearchAnimal>> GetAsync(string animalName);
    }
}
