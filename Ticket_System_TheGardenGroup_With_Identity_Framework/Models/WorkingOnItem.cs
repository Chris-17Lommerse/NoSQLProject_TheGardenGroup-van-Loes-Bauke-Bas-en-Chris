using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ticket_System_TheGardenGroup.Models
{
    public class WorkingOnItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public ObjectId Id { get; set; }
    }
}
