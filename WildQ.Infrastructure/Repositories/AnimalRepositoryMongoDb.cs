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
        private IMongoDatabase _database = Data.MongoDb.GetClient().GetDatabase("WildQDb");
        public async Task CreateAsync(Animal animal)
        {
            try
            {
                var animalCollection = _database.GetCollection<Animal>("animals").InsertOneAsync(animal);

                await animalCollection;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Couldn't create Animal");
                Console.WriteLine(ex);
            }
        }

        public async Task DeleteAsync(Animal animal)
        {
            try
            {
                var filter = Builders<Animal>.Filter.Eq(x => x.Id, animal.Id);

                var animalCollection = _database.GetCollection<Animal>("animals").DeleteOneAsync(filter); //Only need filter

                await animalCollection;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Couldn't delete the animal");
                Console.WriteLine(ex);
            }
            
        }

        public async Task<List<Animal>> GetAllAsync() 
        {
            List<Animal> animals = new List<Animal>();
            try
            {
                var animalCollection = _database.GetCollection<Animal>("animals");

                //Converting ImongoCollection to a list
                return await animalCollection
                .Find(_ => true)
                .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Couldn't find any animal in the database");
                
            }
            return animals; // Returns an empty list of it doesn't go through
        }

        public async Task UpdateAsync(Animal animal)
        {
            try
            {
                // Creating a filter that looks on Id and compares it to animal id
                var filter = Builders<Animal>.Filter.Eq(x => x.Id, animal.Id);

                var animalCollection = _database.GetCollection<Animal>("animals").ReplaceOneAsync(filter, animal);

                await animalCollection;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Couldn't update animal");
                Console.WriteLine(ex);
            }
            
        }
    }
}
