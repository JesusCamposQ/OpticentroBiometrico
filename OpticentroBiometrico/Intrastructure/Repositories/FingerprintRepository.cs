using MongoDB.Bson;
using MongoDB.Driver;
using OpticentroBiometrico.Common;
using OpticentroBiometrico.Intrastructure.Data;
using System;
using System.Collections.Generic;

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

        public List<(string employeeId, byte[] template)> GetEmployeesWithTemplates()
        {
            var result = new List<(string, byte[])>();

            var filter = Builders<BsonDocument>.Filter.And(
                Builders<BsonDocument>.Filter.Eq("isActive", true),
                Builders<BsonDocument>.Filter.Exists("huella", true),
                Builders<BsonDocument>.Filter.Ne("huella", BsonNull.Value),
                Builders<BsonDocument>.Filter.Ne("huella", BsonString.Empty)
            );

            var projection = Builders<BsonDocument>.Projection
                .Include("_id")
                .Include("huella");

            var documents = _usersCollection.Find(filter).Project(projection).ToList();

            foreach (var doc in documents)
            {
                string employeeId = doc["_id"].ToString();
                string base64 = doc.GetValue("huella", "").AsString;

                if (string.IsNullOrWhiteSpace(base64))
                    continue;

                byte[] templateBytes;
                try
                {
                    templateBytes = Convert.FromBase64String(base64.Trim());
                }
                catch (FormatException ex)
                {
                    Logger.Log($"Template Base64 invalido para empleado {employeeId}: {ex.Message}");
                    continue;
                }

                result.Add((employeeId, templateBytes));
            }

            return result;
        }
    }
}
