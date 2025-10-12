using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Ticket_System_TheGardenGroup.Models;
using Ticket_System_TheGardenGroup.Models.Enums;

namespace Ticket_System_TheGardenGroup.ViewModels
{
    public class EmployeeTicketsViewModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public ObjectId Id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("employee_number")]
        public ObjectId EmployeeNumber { get; set; }
        [BsonRepresentation(BsonType.String)]
        [BsonElement("emailaddress")]
        public string EmailAddress { get; set; } = "";
        [BsonRepresentation(BsonType.String)]
        [BsonElement("name")]
        public string Name { get; set; } = "";
        [BsonRepresentation(BsonType.String)]
        [BsonElement("surname")]
        public string Surname { get; set; } = "";
        [BsonElement("employee_role")]
        public EmployeeRole EmployeeRole { get; set; } = EmployeeRole.Regular_Employee;

        [BsonElement("TotalTickets")]
        public int TotalTickets { get; set; } = 0;
    }
}
