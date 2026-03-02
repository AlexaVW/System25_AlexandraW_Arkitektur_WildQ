using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Interfaces;
using Domain.Entities.Models.MongoDbModels;
using MongoDB.Driver;

namespace WildQ.Infrastructure.Repositories
{
    public class AnimalRepositoryMongoDb : IAnimalRepository
    {
        private MongoClient _mongoClient = Data.MongoDb.GetClient(); //Getting client //tabort?
        private IMongoDatabase _database = Data.MongoDb.GetClient().GetDatabase("WildQDb");
        public async Task CreateAsync(Animal animal)
        {
            var animalCollection = _database.GetCollection<Animal>("animals").InsertOneAsync(animal);

            await animalCollection;
            
        }

        public async Task DeleteAsync(Animal animal)
        {
            var filter = Builders<Animal>.Filter.Eq(x => x.Id, animal.Id);

            var animalCollection = _database.GetCollection<Animal>("animals").DeleteOneAsync(filter); //Only need filter

            await animalCollection;
        }

        public async Task<List<Animal>> GetAllAsync() // Keep async?
        {
            var animalCollection = _database.GetCollection<Animal>("animals");

            //Converting ImongoCollection to a list
            return await animalCollection
            .Find(_ => true)
            .ToListAsync(); 
        }

        public async Task UpdateAsync(Animal animal)
        {
            // Creating a filter that looks on Id and compares it to animal id
            var filter = Builders<Animal>.Filter.Eq(x => x.Id, animal.Id);

            var animalCollection = _database.GetCollection<Animal>("animals").ReplaceOneAsync(filter, animal); 

            await animalCollection;
        }
    }
}
