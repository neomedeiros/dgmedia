using dgmedia.Entities.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace dgmedia.Entities.Domain
{
    public class ActionEntity
    {
        public ObjectId Id { get; set; }

        [BsonElement("PartitionKey")]
        public int PartitionKey { get; set; }

        [BsonElement("RowKey")]
        public string RowKey { get; set; }

        [BsonElement("Timestamp")]
        public DateTime Timestamp { get; set; }

        [BsonElement("ActionDataId")]
        public int ActionDataId { get; set; }

        [BsonElement("ActionId")]
        public int ActionId { get; set; }

        [BsonElement("ActionStart")]
        public DateTime ActionStart { get; set; }

        [BsonElement("ActionType")]
        public ActionType ActionType { get; set; }

        [BsonElement("ActionUrl")]
        public string ActionUrl { get; set; }

        [BsonElement("EarnType")]
        public int EarnType { get; set; }

        [BsonElement("Honey")]
        public int Honey { get; set; }

        [BsonElement("IP")]
        public string IP { get; set; }

        [BsonElement("Nectar")]
        public int Nectar { get; set; }

        [BsonElement("StartDate")]
        public DateTime StartDate { get; set; }

        [BsonElement("TenantId")]
        public Tenant TenantId { get; set; }

        [BsonElement("UserId")]
        public int UserId { get; set; }

        [BsonElement("Description")]
        public string Description { get; set; }
    }
}
