using System.Diagnostics;
using MongoDB.Driver;
using web_gallery.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace web_gallery.Services
{
     public abstract class MongoService<TId, DbElement, IDbSettings>
        where DbElement : Models.Model<TId>
        where IDbSettings : IDatabaseSettings
    {
        private readonly IMongoCollection<DbElement> _elements;

        public MongoService(IDbSettings settings, string collectionName)
        {
            Debug.WriteLine($"Service getting database: {settings.DatabaseName} from connection: {settings.ConnectionString}");
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _elements = database.GetCollection<DbElement>(collectionName);
            Debug.WriteLine($"Getting database was successful: {_elements}");
        }

        public List<DbElement> Get() => 
            _elements.Find(element => true).ToList();

        public DbElement Get(string id) =>
            _elements.Find<DbElement>(element => element!.Id!.ToString() == id).FirstOrDefault();

        public DbElement Create(DbElement element)
        {
            _elements.InsertOne(element);
            return element;
        }

        public void Update(string id, DbElement elementIn) =>
            _elements.ReplaceOne(element => element.Id!.ToString() == id, elementIn);

        public void Remove(DbElement elementIn) =>
            _elements.DeleteOne(element => element.Id!.ToString() == elementIn.Id!.ToString());

        public void Remove(string id) => 
            _elements.DeleteOne(element => element.Id!.ToString() == id);
    }

    public abstract class MongoService<DbElement, IDbSettings> : MongoService<string, DbElement, IDbSettings>
        where DbElement : Models.Model<string>
        where IDbSettings : IDatabaseSettings
    {
        public MongoService(IDbSettings settings, string collectionName) : base(settings, collectionName) {}
    }

    // Media
    public class AlbumService : MongoService<Models.Media.Album, IMediaDatabaseSettings>
    {
        public AlbumService(IMediaDatabaseSettings settings) : base(settings, settings.AlbumCollectionName) {}
    }
    
    public class VideoService : MongoService<Models.Media.Video, IMediaDatabaseSettings>
    {
        public VideoService(IMediaDatabaseSettings settings) : base(settings, settings.VideoCollectionName) {}
    }

    // Users
    public class UserService : MongoService<MongoDB.Bson.ObjectId, Models.Users.User, IUsersDatabaseSettings>
    {
        public UserService(IUsersDatabaseSettings settings) : base(settings, settings.UserCollectionName) {}
    }

    public class TokenService : MongoService<Models.Users.Token, IUsersDatabaseSettings>
    {
        public TokenService(IUsersDatabaseSettings settings) : base(settings, settings.TokenCollectionName) {}
    }
}
