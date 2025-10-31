using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Ticket_System_TheGardenGroup_With_Identity_Framework.Models.Enums;

namespace Ticket_System_TheGardenGroup_With_Identity_Framework.ViewModels
{
    public class EmployeeTicketsVm
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public ObjectId Id { get; set; }
        [BsonRepresentation(BsonType.Int32)]
        [BsonElement("employee_number")]
        public int EmployeeNumber { get; set; }
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
