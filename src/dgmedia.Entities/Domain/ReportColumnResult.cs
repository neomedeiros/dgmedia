using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dgmedia.Entities.Domain
{    
    public class Id
    {
        public int _id { get; set; }
    }

    public class ReportColumnResult
    {
        public Id _id { get; set; }
        public int total { get; set; }
    }
}
