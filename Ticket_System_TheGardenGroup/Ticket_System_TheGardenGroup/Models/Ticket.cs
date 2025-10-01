using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Ticket_System_TheGardenGroup.Models.Enums;

namespace Ticket_System_TheGardenGroup.Models
{
    public class Ticket
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string TicketId { get; set; } = "";
        public DateTime CreationTime { get; set; } = DateTime.Now;
        public TicketStatus TicketStatus { get; set; } = TicketStatus.Open;
        public string TicketName { get; set; } = "";
        public string Description { get; set; } = "";
        public bool IsSolved { get; set; } = false;
        public string TicketEscalationDescription { get; set; } = "";
    }
}
