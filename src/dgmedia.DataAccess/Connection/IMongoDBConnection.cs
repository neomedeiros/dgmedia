using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dgmedia.DataAccess.Connection
{
    public interface IMongoDBConnection
    {
        MongoClientSettings MongoClientSettings { get; set; }

        string DatabaseName { get; set; }
    }
}
