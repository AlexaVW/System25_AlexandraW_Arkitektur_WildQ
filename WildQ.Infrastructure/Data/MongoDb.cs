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
            //const string connectionUri = "mongodb+srv://alexandrawestas1999_db_user:Quizquiz123@cluster0.ccnelh2.mongodb.net/?appName=Cluster0";
            const string connectionUri = "mongodb://alexandrawestas1999_db_user:Quizquiz123@ac-9tpppfl-shard-00-00.ccnelh2.mongodb.net:27017,ac-9tpppfl-shard-00-01.ccnelh2.mongodb.net:27017,ac-9tpppfl-shard-00-02.ccnelh2.mongodb.net:27017/?ssl=true&replicaSet=atlas-3ovr6p-shard-0&authSource=admin&appName=Cluster0";
            var settings = MongoClientSettings.FromConnectionString(connectionUri);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);
            return client;
        }
    }
}
