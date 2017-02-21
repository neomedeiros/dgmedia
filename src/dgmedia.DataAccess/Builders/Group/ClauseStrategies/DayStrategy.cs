using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dgmedia.DataAccess.Builders.Group.ClauseStrategies
{
    public class DayStrategy : IClauseStrategy
    {
        public void Fill(GroupBuilder builder)
        {
            
            builder.BuildIdClause("year", "{ '$year': '$StartDate' }");
            builder.BuildIdClause("month", "{ '$month': '$StartDate' }");
            builder.BuildIdClause("day", "{ '$dayOfMonth': '$StartDate' }");
        }
    }
}
