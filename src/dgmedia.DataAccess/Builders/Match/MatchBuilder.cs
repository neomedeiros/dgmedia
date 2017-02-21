using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dgmedia.DataAccess.Builders.Match
{
    public class MatchBuilder
    {
        private BsonDocument _document { get; set; }

        public BsonDocument Document
        {
            get
            {
                return _document;
            }
        }

        private Dictionary<MatchType, MatchClauseStrategy> Strategies { get; set; }

        public MatchBuilder()
        {
            Strategies = new Dictionary<MatchType, MatchClauseStrategy>();

            Strategies.Add(MatchType.DateRange, new DateRangeStrategy());
            Strategies.Add(MatchType.String, new StringStrategy());
            Strategies.Add(MatchType.Integer, new IntegerStrategy());

            _document = new BsonDocument();
        }

        public void BuildClause(string name, object value, MatchType type)
        {
            if (value != null)
                _document.Add(name, Strategies[type].Build(value));
        }        
    }
}
