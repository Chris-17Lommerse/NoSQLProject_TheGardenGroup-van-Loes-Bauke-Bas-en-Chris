using MongoDB.Bson.Serialization.Attributes;

namespace Ticket_System_TheGardenGroup.Models
{
    public class EmployeeNumber
    {
        [BsonElement("number")]
        public int Number { get; set; } = 0;
    }
}
