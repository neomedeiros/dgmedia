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
using dgmedia.DataAccess.Builders.Group;

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
            return GetCollection().Distinct<Tenant>("TenantId", Builders<ActionEntity>.Filter.Ne(m => m.TenantId, null)).ToList();
        }

        public IEnumerable<ResultChartInterval> GetActionIntervals()
        {
            return new ResultChartInterval[] { ResultChartInterval.Hour, ResultChartInterval.Day, ResultChartInterval.Month };
        }        

        public IEnumerable<BsonDocument> GetActionChart(ActionsChartConfiguration chartConfiguration)
        {            
            var aggregate = GetCollection().Aggregate()
                .Match(GetMatchDocument(chartConfiguration))
                .Group(GetGroupDocument(chartConfiguration));

            return aggregate.ToList();
        }

        private BsonDocument GetMatchDocument(ActionsChartConfiguration chartConfiguration)
        {
            var matchBuilder = new MatchBuilder();

            matchBuilder.BuildClause("ActionStart", chartConfiguration.SelectedActionStart, MatchType.DateRange);
            matchBuilder.BuildClause("ActionType", chartConfiguration.SelectedActionTypes, MatchType.Integer);
            matchBuilder.BuildClause("UserId", chartConfiguration.SelectedUserIDs, MatchType.Integer);
            matchBuilder.BuildClause("EarnType", chartConfiguration.SelectedEarnTypes, MatchType.Integer);
            matchBuilder.BuildClause("IP", chartConfiguration.SelectedIPS, MatchType.String);
            matchBuilder.BuildClause("StartDate", chartConfiguration.SelectedStartDate, MatchType.DateRange);

            return matchBuilder.Document;
        }

        private BsonDocument GetGroupDocument(ActionsChartConfiguration chartConfiguration)
        {
            var groupClauseStrategy = new GroupClauseStrategy();
            var groupBuilder = new GroupBuilder();

            groupBuilder.BuildIdClause("'userId'", "'$UserId'");

            groupClauseStrategy.Fill(groupBuilder, chartConfiguration.ResultChartInterval);

            groupBuilder.BuildResultClause("$sum", (chartConfiguration.ResultChartType == ResultChartType.Events ? "1" : "\"$Nectar\""));
            return groupBuilder.Document;
        }
    }
}
