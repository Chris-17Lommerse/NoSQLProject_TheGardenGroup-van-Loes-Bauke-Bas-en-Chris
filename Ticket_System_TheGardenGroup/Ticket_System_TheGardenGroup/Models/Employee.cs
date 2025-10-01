using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Ticket_System_TheGardenGroup.Models
{
    public class Employee
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public int EmployeeNumber { get; set; } = 0;
        public string Password { get; set; } = "";
        public EmployeeRole EmployeeRole { get; set; } = EmployeeRole.RegularEmployee;
        public string Name { get; set; } = "";
        public string Surname { get; set; } = "";
        public string EmailAddress { get; set; } = "";
    }
}
