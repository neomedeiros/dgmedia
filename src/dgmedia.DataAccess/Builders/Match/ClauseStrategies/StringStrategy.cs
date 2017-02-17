using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace dgmedia.DataAccess.Builders.Match
{
    public class StringStrategy : MatchClauseStrategy
    {
        public override BsonDocument Build(object value)
        {            
            if (!(value is IEnumerable<string>))
                throw new ArgumentException("Invalid type of value parameter. It should be an IEnumerable<string>.");

            var strings = (IEnumerable<string>)value;

            if (!strings.Any()) return BsonDocument.Create(string.Empty);

            return BsonDocument.Parse(string.Format("{{$in: {0}}}", strings.ToJson()));
        }
    }
}
