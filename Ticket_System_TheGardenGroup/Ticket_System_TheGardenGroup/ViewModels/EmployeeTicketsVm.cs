using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Ticket_System_TheGardenGroup.Models.Enums;

namespace Ticket_System_TheGardenGroup.ViewModels
{
    public class EmployeeTicketsVm
    {
        [BsonRepresentation(BsonType.Int32)]
        [BsonElement("employee_number")]
        public int EmployeeNumber { get; set; } = 0;

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
        public EmployeeRole EmployeeRole { get; set; } = EmployeeRole.REGULAR_EMPLOYEE;

        [BsonElement("TotalTickets")]
        public int TotalTickets { get; set; } = 0;

    }
}
