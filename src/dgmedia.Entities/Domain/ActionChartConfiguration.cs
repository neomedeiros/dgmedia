using dgmedia.Entities.CustomTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dgmedia.Entities.Domain
{
    public class ActionsChartConfiguration
    {
        public DateRange SelectedActionStart { get; set; }
        public IEnumerable<int> SelectedActionTypes { get; set; }
        public IEnumerable<int> SelectedEarnTypes { get; set; }
        public DateRange SelectedStartDate { get; set; }
        public IEnumerable<string> SelectedIPS { get; set; }
        public IEnumerable<int> SelectedUserIDs { get; set; }       
    }
}
