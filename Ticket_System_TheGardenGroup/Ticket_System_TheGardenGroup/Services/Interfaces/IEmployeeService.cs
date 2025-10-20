using MongoDB.Bson;
using Ticket_System_TheGardenGroup.Models;
using Ticket_System_TheGardenGroup.ViewModels;

namespace Ticket_System_TheGardenGroup.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<EmployeeTicketsVm>> GetAllEmployeesWithAmountOfTicketsAsync();
        Task<EmployeeViewModel> GetEmployeeAsync(int employeeNumber);
        void AddEmployee(Employee employee);

		Task<Employee> GetEmployeeByNumberAsync(string employeeNumber);

        //for finding a service desk employee to embed
        Task<EmbeddedEmployee> GetActiveEmbeddedSdEmployeeByIdAsync(int employeeNumber);

        Task<Employee> GetToBe

    }
}
