using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dgmedia.DataAccess.Builders.Match
{
    public abstract class MatchClauseStrategy
    {
        public abstract BsonDocument Build(object value);
    }
}
