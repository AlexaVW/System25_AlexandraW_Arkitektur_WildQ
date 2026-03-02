using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Models.MongoDbModels;

namespace WildQ.Application.Interfaces
{
    public interface IAnimalService
    {
        Task<List<Animal>> GetAllAnimalsAsync();
        Task<Animal> GetAnimalAsync(int animalId);
        Task CreateAnimalAsync(Animal animal);
        Task UpdateAnimalAsync(Animal animal);
        Task DeleteAnimalAsync(Animal animal);

    }
}
