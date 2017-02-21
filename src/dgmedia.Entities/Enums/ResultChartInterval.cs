using dgmedia.Util.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dgmedia.Entities.Enums
{
    public enum ResultChartInterval
    {
        [Description("Hour")]
        Hour,
        [Description("Day")]
        Day,
        [Description("Month")]
        Month
    }
}
