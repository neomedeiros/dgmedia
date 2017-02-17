using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace dgmedia.DataAccess.Builders.Match
{
    public class IntegerStrategy : MatchClauseStrategy
    {
        public override BsonDocument Build(object value)
        {             
            if (!(value is IEnumerable<int>))
                throw new ArgumentException("Invalid type of value parameter. It should be an IEnumerable<int>.");

            var ints = (IEnumerable<int>)value;

            if (!ints.Any()) return new BsonDocument();

            return BsonDocument.Parse(string.Format("{{$in: {0}}}", ints.ToJson()));
        }
    }
}
