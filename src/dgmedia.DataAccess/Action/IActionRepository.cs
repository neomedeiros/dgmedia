using dgmedia.Entities.Domain;
using dgmedia.Entities.Enums;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dgmedia.DataAccess.Action
{
    public interface IActionRepository
    {
        IEnumerable<ActionType> GetActionTypes();

        IEnumerable<int> GetEarnTypes();

        IEnumerable<string> GetIPS();

        IEnumerable<int> GetUserIDs();

        IEnumerable<Tenant> GetTenants();

        IEnumerable<BsonDocument> GetActionChart(ActionsChartConfiguration chartConfiguration);
        
    }
}
