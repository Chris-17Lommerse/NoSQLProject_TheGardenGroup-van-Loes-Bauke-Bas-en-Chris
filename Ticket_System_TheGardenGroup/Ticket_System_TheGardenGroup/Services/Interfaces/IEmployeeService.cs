using Ticket_System_TheGardenGroup.Models;
using Ticket_System_TheGardenGroup.ViewModels;

namespace Ticket_System_TheGardenGroup.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<EmployeeTicketsViewModel>> GetAllEmployeesWithAmountOfTicketsAsync();
        //Task<EmployeeViewModel> GetEmployeeAsync();
    }
}
