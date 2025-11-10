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
        public EmbeddedEmployee()
        {
            
        }

        public EmbeddedEmployee(int employeeNumber, EmployeeRole employeeRole, string emailAddress, string name)
        {
            EmployeeNumber = employeeNumber;
            EmployeeRole = employeeRole;
            EmailAddress = emailAddress;
            Name = name;
        }
        public override string ToString()
        {
            return $"employeeNumber: {EmployeeNumber}, employeeRole: {EmployeeRole}, emailAddress: {EmailAddress}, name: {Name}";
        }
    }
}
