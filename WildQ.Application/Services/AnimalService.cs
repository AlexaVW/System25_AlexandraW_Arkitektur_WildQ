using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Interfaces;
using Domain.Entities.Models.MongoDbModels;
using WildQ.Application.Interfaces;
using WildQ.Infrastructure.Repositories;

namespace WildQ.Application.Services
{
    public class AnimalService : IAnimalService
    {
        IAnimalRepository _animalRepository = new AnimalRepositoryMongoDb(); //Before dependency injection
        public async Task<List<Animal>> GetAllAnimalsAsync()
        {
            return await _animalRepository.GetAllAsync();
        }

        public async Task<Animal> GetAnimalAsync(string animalId)
        {
            throw new NotImplementedException();
        }
        public async Task CreateAnimalAsync(Animal animal)
        {
            await _animalRepository.CreateAsync(animal);
        }

        public async Task UpdateAnimalAsync(Animal animal)
        {
            await _animalRepository.UpdateAsync(animal);
        }

        public async Task DeleteAnimalAsync(Animal animal)
        {
            await _animalRepository.DeleteAsync(animal);
        }
    }
}
