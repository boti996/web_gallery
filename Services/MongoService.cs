using System.Diagnostics;
using MongoDB.Driver;
using web_gallery.Models;
using System.Collections.Generic;
using System.Linq;

namespace web_gallery.Services
{
     public class MongoService<DbElement, IDbSettings>
        where DbElement : Models.Model
        where IDbSettings : IDatabaseSettings
    {
        private readonly IMongoCollection<DbElement> _elements;

        public MongoService(IDbSettings settings, string collectionName)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _elements = database.GetCollection<DbElement>(collectionName);
        }

        public List<DbElement> Get() => 
            _elements.Find(element => true).ToList();

        public DbElement Get(string id) =>
            _elements.Find<DbElement>(element => element.Id == id).FirstOrDefault();

        public DbElement Create(DbElement element)
        {
            _elements.InsertOne(element);
            return element;
        }

        public void Update(string id, DbElement elementIn) =>
            _elements.ReplaceOne(element => element.Id == id, elementIn);

        public void Remove(DbElement elementIn) =>
            _elements.DeleteOne(element => element.Id == elementIn.Id);

        public void Remove(string id) => 
            _elements.DeleteOne(element => element.Id == id);
    }
}
