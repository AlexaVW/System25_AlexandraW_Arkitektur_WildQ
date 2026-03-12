using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        // Getting the client and getting the database
        private IMongoDatabase _database = Data.MongoDb.GetClient().GetDatabase("WildQDb");
        public async Task CreateAsync(Animal animal)
        {
            try
            {
                // Getting collection of animals and creating the animal
                var animalCollection = _database.GetCollection<Animal>("animals").InsertOneAsync(animal);

                await animalCollection;
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException(nameof(animal), "Couldn't create animal. Animal is null. You have to pass in a valid animal object");
            }

            catch (Exception ex)
            {
                throw new Exception("Error\n" + ex.Message);
            }
        }

        public async Task DeleteAsync(Animal animal)
        {
            try
            {
                // Creating a filter to see which animal we are on right now
                var filter = Builders<Animal>.Filter.Eq(x => x.Id, animal.Id);

                var animalCollection = _database.GetCollection<Animal>("animals").DeleteOneAsync(filter);

                await animalCollection;
            }
            catch(ArgumentNullException e)
            {
                throw new ArgumentNullException(nameof(animal), "Couldn't delete animal. Animal is null. You have to pass in a valid animal object");
            }
            catch (Exception ex)
            {
                throw new Exception("Error\n" + ex.Message);
            }
        }

        public async Task<List<Animal>> GetAllAsync() 
        {
            List<Animal> animals = new List<Animal>();
            try
            {
                // Getting the collection of animals from MongoDb
                var animalCollection = _database.GetCollection<Animal>("animals"); 

                // Finding the documents and returning it as a list
                return await animalCollection
                .Find(_ => true) 
                .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Couldn't find any animal in the database");
                
            }
            return animals; // Returns an empty list if it doesn't go through
        }

        public async Task UpdateAsync(Animal animal)
        {
            try
            {
                // Creating a filter that looks at the Id and compares it to animal id
                var filter = Builders<Animal>.Filter.Eq(x => x.Id, animal.Id);

                var animalCollection = _database.GetCollection<Animal>("animals").ReplaceOneAsync(filter, animal);

                await animalCollection;
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException(nameof(animal), "Couldn't update animal. Animal is null. You have to pass in an animal object that not null");
            }
            catch (Exception ex)
            {
                throw new Exception("Error\n" + ex.Message);
            }
        }
    }
}
