using MongoDB.Bson;
using MongoDB.Driver;
using OpticentroBiometrico.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpticentroBiometrico.Intrastructure.Data
{
    internal class EmployeeRepository
    {
        private readonly IMongoCollection<BsonDocument> _usersCollection;
        public EmployeeRepository()
        {
            var connection = new MongoConnection();
            var database = connection.GetDatabase();

            _usersCollection = database.GetCollection<BsonDocument>("users");
        }
        public List<Employee> GetActiveEmployees()
        {
            var employees = new List<Employee>();

            var filter = Builders<BsonDocument>.Filter.And(
                Builders<BsonDocument>.Filter.Eq("isActive", true),
                Builders<BsonDocument>.Filter.Exists("tipo",false)
             );

            var documents = _usersCollection.Find(filter).ToList();

            foreach (var doc in documents)
            {
                employees.Add(new Employee
                {
                    Id = doc["_id"].ToString(),
                    Nombre = doc.GetValue("nombre", "").ToString(),
                    ApPaterno = doc.GetValue("ap_paterno", "").ToString(),
                    ApMaterno = doc.GetValue("ap_materno", "").ToString(),
                    Ci = doc.GetValue("ci", "").ToString(),
                    IsActive = doc.GetValue("isActive", false).AsBoolean
                });
            }

            return employees;
        }
    }
}
