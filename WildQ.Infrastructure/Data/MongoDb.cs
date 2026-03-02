using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Models.MongoDbModels;
using MongoDB.Driver;


namespace WildQ.Infrastructure.Data
{
    public class MongoDb
    {
        public static MongoClient GetClient()
        {
            const string connectionUri = "mongodb+srv://alexandrawestas1999_db_user:Quizquiz123@cluster0.ccnelh2.mongodb.net/?appName=Cluster0";
            var settings = MongoClientSettings.FromConnectionString(connectionUri);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);
            return client;
        }

        //private static IMongoCollection<Animal> GetAnimalCollection()
        //{
        //    var client = GetClient();

        //    var database = client.GetDatabase("WildQDb");

        //    var animalCollection = database.GetCollection<Animal>("animals");

        //    return animalCollection; //Returns an IMongoCollection
        //}
    }
}
