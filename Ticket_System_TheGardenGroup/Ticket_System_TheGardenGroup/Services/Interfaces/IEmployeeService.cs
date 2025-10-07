using Ticket_System_TheGardenGroup.Models;

namespace Ticket_System_TheGardenGroup.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetAllEmployees();
    }
}
