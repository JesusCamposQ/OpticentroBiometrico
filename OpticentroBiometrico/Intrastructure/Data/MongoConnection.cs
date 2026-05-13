using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace OpticentroBiometrico.Intrastructure.Data
{
    internal class MongoConnection
    {
        private readonly IMongoDatabase _database;
        public MongoConnection()
        {
            var connectionString = ConfigurationManager.AppSettings["MongoConnectionString"];
            var databaseName = ConfigurationManager.AppSettings["MongoDatabaseName"];

            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }
        public IMongoDatabase GetDatabase()
        {
            return _database;
        }
    }
}
