using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Ticket_System_TheGardenGroup.Models.Enums;

namespace Ticket_System_TheGardenGroup.Models
{
    public class EmbeddedEmployee
    {
        [BsonElement("employee_number")]
        public EmployeeNumber EmployeeNumber { get; set; } = new EmployeeNumber();
        [BsonElement("employee_role")]
        public EmployeeRole EmployeeRole { get; set; } = EmployeeRole.REGULAR_EMPLOYEE;
        [BsonElement("emailaddress")]
        public string EmailAddress { get; set; } = "";
        [BsonElement("name")]
        public string Name { get; set; } = "";
        [BsonElement("surname")]
        public string Surname { get; set; } = "";
        [BsonElement("password")]
        public string Password { get; set; } = "";
        [BsonElement("isActive")]
        public bool IsActive { get; set; } = false;
    }
}
