using dgmedia.DataAccess.Builders.Group.ClauseStrategies;
using dgmedia.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dgmedia.DataAccess.Builders.Group
{
    public class GroupClauseStrategy
    {
        private Dictionary<ResultChartInterval, IClauseStrategy> _strategies;

        public GroupClauseStrategy()
        {
            _strategies = new Dictionary<ResultChartInterval, IClauseStrategy>();
            _strategies.Add(ResultChartInterval.Day, new DayStrategy());
            _strategies.Add(ResultChartInterval.Month, new MonthStrategy());
            _strategies.Add(ResultChartInterval.Hour, new HourStrategy());
        }

        public void Fill(GroupBuilder builder, ResultChartInterval intervalType)
        {
            _strategies[intervalType].Fill(builder);
        }
    }
}
