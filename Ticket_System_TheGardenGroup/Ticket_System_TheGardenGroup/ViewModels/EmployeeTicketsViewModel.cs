using MongoDB.Bson;
using Ticket_System_TheGardenGroup.Models.Enums;

namespace Ticket_System_TheGardenGroup.ViewModels
{
    public class EmployeeTicketsViewModel
    {
        public ObjectId Id { get; set; }
        public string EmailAddress { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public EmployeeRole EmployeeRole { get; set; } = EmployeeRole.REGULAR_EMPLOYEE;
        public int TotalTickets { get; set; } = 0;
    }
}
