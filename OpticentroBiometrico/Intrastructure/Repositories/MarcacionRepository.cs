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
            _marcacionesCollection = database.GetCollection<BsonDocument>("Marcacion");
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

        public BsonDocument GetLastMarcacion(string employeeId)
        {
            try
            {
                var objectId = ObjectId.Parse(employeeId);
                var filter = Builders<BsonDocument>.Filter.Eq("user", objectId);
                var sort = Builders<BsonDocument>.Sort.Descending("fecha");

                var last = _marcacionesCollection.Find(filter)
                                                 .Sort(sort)
                                                 .Limit(1)
                                                 .FirstOrDefault();

                if (last == null)
                {
                    Logger.Log($"No se encontró ninguna marcación para el empleado {employeeId}");
                }

                return last;
            }
            catch (FormatException fe)
            {
                Logger.Log($"ID de empleado inválido {employeeId}: {fe.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Logger.Log($"Error al recuperar última marcación para empleado {employeeId}: {ex.Message}");
                return null;
            }
        }
    }
}
