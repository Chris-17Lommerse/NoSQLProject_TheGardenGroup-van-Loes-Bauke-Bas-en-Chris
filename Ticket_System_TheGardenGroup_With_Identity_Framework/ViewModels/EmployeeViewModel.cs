using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Ticket_System_TheGardenGroup_With_Identity_Framework.Models;
using Ticket_System_TheGardenGroup_With_Identity_Framework.Models.Enums;

namespace Ticket_System_TheGardenGroup_With_Identity_Framework.ViewModels
{
    public class EmployeeViewModel
    {
        public ObjectId Id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
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
        [BsonElement("is_active")]
        public bool IsActive { get; set; } = false;
        public EmployeeViewModel(Employee employee)
        {
            EmployeeNumber = employee.EmployeeNumber;
            EmailAddress = employee.EmailAddress;
            Name = employee.Name;
            Surname = employee.Surname;
            EmployeeRole = employee.EmployeeRole;
            IsActive = employee.IsActive;
        }

        public EmployeeViewModel(int employeeNumber, string emailAddress, string name, string surname, EmployeeRole employeeRole, bool isActive)
        {
            EmployeeNumber = employeeNumber;
            EmailAddress = emailAddress;
            Name = name;
            Surname = surname;
            EmployeeRole = employeeRole;
            IsActive = isActive;
        }

        public EmployeeViewModel()
        {

        }
    }
}
