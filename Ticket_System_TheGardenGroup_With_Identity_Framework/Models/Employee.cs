using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Ticket_System_TheGardenGroup_With_Identity_Framework.Models.Enums;

namespace Ticket_System_TheGardenGroup_With_Identity_Framework.Models
{
    public class Employee
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public ObjectId Id { get; set; }

        [BsonElement("employee_number")]
        public int EmployeeNumber { get; set; } = 0;

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

        [BsonElement("is_active")]
        public bool IsActive { get; set; } = false;

        [BsonElement("workingOn")]
        public ObjectId[] WorkingOn { get; set; } = Array.Empty<ObjectId>();
    }
}
