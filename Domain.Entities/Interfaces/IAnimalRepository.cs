using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Models.MongoDbModels;

namespace Domain.Entities.Interfaces
{
    public interface IAnimalRepository
    {
        Task<List<Models.MongoDbModels.Animal>> GetAllAsync();

        Task CreateAsync(Animal animal);

        Task UpdateAsync(Animal animal);

        Task DeleteAsync(Animal animal);
    }
}
