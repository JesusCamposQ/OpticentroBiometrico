using MongoDB.Bson;
using MongoDB.Driver;
using OpticentroBiometrico.Intrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpticentroBiometrico.Intrastructure.Repositories
{
    internal class FingerprintRepository
    {
        private readonly IMongoCollection<BsonDocument> _usersCollection;

        public FingerprintRepository()
        {
            var database = new MongoConnection().GetDatabase();
            _usersCollection = database.GetCollection<BsonDocument>("users");
        }

        public void SaveFingerprint(string employeeId, string template)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(employeeId));
            var update = Builders<BsonDocument>.Update.Set("huella", template);
            _usersCollection.UpdateOne(filter, update);
        }
    }
}
