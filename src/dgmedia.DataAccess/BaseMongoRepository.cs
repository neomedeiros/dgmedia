using dgmedia.DataAccess.Connection;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dgmedia.DataAccess
{
    public class BaseMongoRepository<T> where T : class
    {
        private MongoClient _mongoClient;
        internal IMongoDatabase _mongoDatabase;

        public string _collectionName { get; set; }

        public BaseMongoRepository(IMongoDBConnection mongodbConnection, string collectionName)
        {
            _mongoClient = new MongoClient(mongodbConnection.MongoClientSettings);
            _mongoDatabase = _mongoClient.GetDatabase(mongodbConnection.DatabaseName);
            _collectionName = collectionName;
        }

        internal IMongoCollection<T> GetCollection()
        {
            return _mongoDatabase.GetCollection<T>(_collectionName);
        }
    }
}
