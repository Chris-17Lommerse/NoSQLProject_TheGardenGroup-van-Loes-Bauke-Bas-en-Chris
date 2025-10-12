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
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("employee_number")]
        public string EmployeeNumber { get; set; } = "";
        [BsonElement("password")]
        public string Password { get; set; } = "";
        //BEN HIER AAN HET EXPERIMENTEREN
        [BsonElement("employee_role")]
        [BsonRepresentation(BsonType.String)]
        public EmployeeRole EmployeeRole { get; set; } = EmployeeRole.Regular_Employee;

        [BsonElement("name")]
        public string Name { get; set; } = "";
        [BsonElement("surname")]
        public string Surname { get; set; } = "";
        [BsonElement("emailaddress")]
        public string EmailAddress { get; set; } = "";
    }
}
