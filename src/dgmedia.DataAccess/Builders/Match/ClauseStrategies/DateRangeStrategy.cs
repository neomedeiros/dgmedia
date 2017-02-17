using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using dgmedia.Entities.CustomTypes;
using System.Globalization;

namespace dgmedia.DataAccess.Builders.Match
{
    public class DateRangeStrategy : MatchClauseStrategy
    {
        public override BsonDocument Build(object value)
        {            
            if (!(value is DateRange))
                throw new ArgumentException("Invalid type of value parameter. It should be a Date Range.");

            var dateRange = (DateRange)value;            

            return BsonDocument.Parse(
                string.Format(
                    "{{$gte: ISODate('{0}'), $lt: ISODate('{1}')}}",
                    dateRange.StartDate.ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture),
                    dateRange.EndDate.ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture)
                ));
        }
    }
}
