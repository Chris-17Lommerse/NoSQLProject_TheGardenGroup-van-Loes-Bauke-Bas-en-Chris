using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Ticket_System_TheGardenGroup.Models.Enums;

namespace Ticket_System_TheGardenGroup.Models
{
    public class EmbeddedEmployee
    {
        [BsonElement("employee_number")]
        public int EmployeeNumber { get; set; } = 0;

        [BsonElement("employee_role")]
        public EmployeeRole EmployeeRole { get; set; } = EmployeeRole.REGULAR_EMPLOYEE;

        [BsonElement("emailaddress")]
        public string EmailAddress { get; set; } = "";

        [BsonElement("name")]
        public string Name { get; set; } = "";
    }
}
