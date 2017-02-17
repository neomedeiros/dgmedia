using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using dgmedia.DataAccess.Action;
using dgmedia.Entities.Domain;
using MongoDB.Bson;
using dgmedia.Entities.Enums;
using dgmedia.Util.Helpers;
using System.Globalization;
using dgmedia.DataAccess.Connection;
using dgmedia.DataAccess.Builders;
using dgmedia.DataAccess.Builders.Match;

namespace dgmedia.DataAccess
{
    public class ActionRepository : BaseMongoRepository<ActionEntity>, IActionRepository
    {
        public ActionRepository(IMongoDBConnection mongoDBConnection) : base(mongoDBConnection, "ActionEntities") { }

        public IEnumerable<int> GetEarnTypes()
        {
            return GetCollection().Distinct<int>("EarnType", FilterDefinition<ActionEntity>.Empty).ToList();
        }

        public IEnumerable<string> GetIPS()
        {
            return GetCollection().Distinct<string>("IP", FilterDefinition<ActionEntity>.Empty).ToList();
        }

        public IEnumerable<int> GetUserIDs()
        {
            return GetCollection().Distinct<int>("UserId", FilterDefinition<ActionEntity>.Empty).ToList();
        }

        public IEnumerable<ActionType> GetActionTypes()
        {
            return GetCollection().Distinct<ActionType>("ActionType", FilterDefinition<ActionEntity>.Empty).ToList();
        }

        public IEnumerable<Tenant> GetTenants()
        {
            return GetCollection().Distinct<Tenant>("TenantId", FilterDefinition<ActionEntity>.Empty).ToList();
        }

        public IEnumerable<BsonDocument> GetActionChart(ActionsChartConfiguration chartConfiguration)
        {
            var matchBuilder = new MatchBuilder();

            matchBuilder.BuildClause("ActionStart", chartConfiguration.SelectedActionStart, MatchType.DateRange);
            matchBuilder.BuildClause("ActionType", chartConfiguration.SelectedActionTypes, MatchType.Integer);
            matchBuilder.BuildClause("UserId", chartConfiguration.SelectedUserIDs, MatchType.Integer);
            matchBuilder.BuildClause("EarnType", chartConfiguration.SelectedEarnTypes, MatchType.Integer);
            matchBuilder.BuildClause("IP", chartConfiguration.SelectedIPS, MatchType.String);
            matchBuilder.BuildClause("StartDate", chartConfiguration.SelectedStartDate, MatchType.DateRange);
            
            var aggregate = GetCollection().Aggregate()
                .Match(matchBuilder.Document)
                .Group(BsonDocument.Parse(@"
                    {	                
                        _id: {
	  		            'userId': '$UserId',
                        'year': { '$year': '$StartDate' },
                        'month': { '$month': '$StartDate' },
                        'day': { '$dayOfMonth': '$StartDate' },
                        'hour': {'$hour': '$StartDate'}
        	            }, 
                        total:{$sum:1}
                    }"));

            return aggregate.ToList();
        }
    }
}
