using dgmedia.Util.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dgmedia.Entities.Enums
{
    public enum Tenant
    {
        [Description("Earnhoney")]
        earnhoney = 1,

        [Description("Matchedcars")]
        matchedcars = 2,

        [Description("TVMinutes")]
        tvminutes = 3,

        [Description("Miimd")]
        miimd = 4,

        [Description("Earnhoney TV")]
        earnhoneytv = 5,

        [Description("TV Glee")]
        tvglee = 6,

        [Description("Furryflix")]
        furryflix = 7,

        [Description("AutoPairs")]
        autopairs = 8,

        [Description("Moovie Mania")]
        mooviemania = 9,

        [Description("Earnhoney Android")]
        EarnHoneyAndroid = 10,

        [Description("TV Glee Roku")]
        TVGleeRoku = 11,

        [Description("TV Glee Android")]
        TVGleeAndroid = 12
    }
}
