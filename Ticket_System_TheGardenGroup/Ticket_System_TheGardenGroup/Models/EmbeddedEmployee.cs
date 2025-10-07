using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Ticket_System_TheGardenGroup.Models.Enums;

namespace Ticket_System_TheGardenGroup.Models
{
    public class EmbeddedEmployee
    {
        public string EmployeeNumber { get; set; } = "";
        public EmployeeRole EmployeeRole { get; set; } = EmployeeRole.RegularEmployee;
        public string EmailAddress { get; set; } = "";
        public string Name { get; set; } = "";
    }
}
