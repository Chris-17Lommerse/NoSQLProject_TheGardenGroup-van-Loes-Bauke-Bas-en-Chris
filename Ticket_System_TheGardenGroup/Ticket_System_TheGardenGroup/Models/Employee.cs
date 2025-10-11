using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Ticket_System_TheGardenGroup.Models.Enums;

namespace Ticket_System_TheGardenGroup.Models
{
    public class Employee
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string? Id { get; set; }

        [BsonRepresentation(BsonType.Int32)]
        [BsonElement("employee_number")]
        public int EmployeeNumber { get; set; } = 0;

        [BsonElement("password")]
        public string Password { get; set; } = "";

        [BsonElement("employee_role")]
        public EmployeeRole EmployeeRole { get; set; } = EmployeeRole.Regular_employee;

        [BsonElement("name")]
        public string Name { get; set; } = "";

        [BsonElement("surname")]
        public string Surname { get; set; } = "";

        [BsonElement("emailaddress")]
        public string EmailAddress { get; set; } = "";

        [BsonElement("isActive")]
        public bool IsActive { get; set; } = false;
    }
}
