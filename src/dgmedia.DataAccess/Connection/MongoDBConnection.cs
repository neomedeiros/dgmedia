using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dgmedia.DataAccess.Connection
{
    public class MongoDBConnection : IMongoDBConnection
    {
        public MongoClientSettings MongoClientSettings { get; set; }

        public string DatabaseName { get; set; }
    }
}
