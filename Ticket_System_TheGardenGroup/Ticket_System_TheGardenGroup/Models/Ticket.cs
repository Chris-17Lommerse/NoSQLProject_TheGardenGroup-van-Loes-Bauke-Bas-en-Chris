using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Ticket_System_TheGardenGroup.Models.Enums;

namespace Ticket_System_TheGardenGroup.Models
{
    public class Ticket
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string? Id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("ticket_id")]
        public string TicketId { get; set; } = "";
        [BsonElement("creation_time")]
        public DateTime CreationTime { get; set; } = DateTime.Now;
        [BsonElement("ticket_status")]
        public TicketStatus TicketStatus { get; set; } = TicketStatus.Open;
        [BsonElement("ticket_name")]
        public string TicketName { get; set; } = "";
        [BsonElement("description")]
        public string Description { get; set; } = "";
        [BsonElement("is_solved")]
        public bool IsSolved { get; set; } = false;
        [BsonElement("ticket_escalation_description")]
        public string TicketEscalationDescription { get; set; } = "";
        [BsonElement("reporting_employee")]
        public EmbeddedEmployee ReportingEmployee { get; set; } = new EmbeddedEmployee();
        [BsonElement("solving_employee")]
        public EmbeddedEmployee SolvingEmployee { get; set; } = new EmbeddedEmployee();
    }
}
