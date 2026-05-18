using MongoDB.Bson;
using MongoDB.Driver;
using OpticentroBiometrico.Common;
using OpticentroBiometrico.Intrastructure.Data;
using System;

namespace OpticentroBiometrico.Intrastructure.Repositories
{
    internal class MarcacionRepository
    {
        private readonly IMongoCollection<BsonDocument> _marcacionesCollection;

        public MarcacionRepository()
        {
            var database = new MongoConnection().GetDatabase();
            _marcacionesCollection = database.GetCollection<BsonDocument>("marcaciones");
        }

        public bool SaveMarcacion(string employeeId)
        {
            try
            {
                var document = new BsonDocument
                {
                    { "user", ObjectId.Parse(employeeId) },
                    { "fecha", DateTime.UtcNow },
                    { "tipo", "biometrico" }
                };

                _marcacionesCollection.InsertOne(document);
                Logger.Log($"Marcación registrada para empleado {employeeId}");
                return true;
            }
            catch (Exception ex)
            {
                Logger.Log($"Error al guardar marcación para empleado {employeeId}: {ex.Message}");
                return false;
            }
        }
    }
}
