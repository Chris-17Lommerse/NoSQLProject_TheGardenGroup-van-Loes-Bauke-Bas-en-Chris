using MongoDB.Bson.Serialization.Attributes;
using Ticket_System_TheGardenGroup_With_Identity_Framework.Models.Enums;

namespace Ticket_System_TheGardenGroup_With_Identity_Framework.Models
{
    public class EmbeddedEmployee
    {
        [BsonElement("employee_number")]
        public int EmployeeNumber { get; set; } = 0;

        [BsonElement("employee_role")]
        public EmployeeRole EmployeeRole { get; set; } = EmployeeRole.Regular_Employee;

        [BsonElement("emailaddress")]
        public string EmailAddress { get; set; } = "";

        [BsonElement("name")]
        public string Name { get; set; } = "";

        //Waar is dit voor?
        public static implicit operator EmbeddedEmployee(Employee v)
        {
            throw new NotImplementedException();
        }
    }
}
