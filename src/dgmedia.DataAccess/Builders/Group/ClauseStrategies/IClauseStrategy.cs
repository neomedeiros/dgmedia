using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dgmedia.DataAccess.Builders.Group.ClauseStrategies
{
    public interface IClauseStrategy
    {
        void Fill(GroupBuilder builder);
    }
}
