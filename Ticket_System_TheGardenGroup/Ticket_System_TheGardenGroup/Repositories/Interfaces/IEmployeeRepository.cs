using MongoDB.Bson;
using Ticket_System_TheGardenGroup.Models;
using Ticket_System_TheGardenGroup.ViewModels;

namespace Ticket_System_TheGardenGroup.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<List<EmployeeTicketsVm>> GetAllEmployeesWithAmountOfTicketsAsync();
        List<Employee> GetAllRegularEmployees();
        List<Employee> GetAllServiceDeskEmployees();
        void AddEmployee(Employee employee);
        Task<Employee> GetEmployeeAsync(int employeeNumber);
        Task RemoveRegularEmployee(Employee employee);
        Task RemoveServiceDeskEmployee(Employee employee);
        Task UpdateRegularEmployee(Employee employee);
        Task UpdateServiceDeskEmployee(Employee employee);

        //for finding a service desk employee to embed
        Task<EmbeddedEmployee> GetActiveEmbeddedSdEmployeeByIdAsync(int employeeNumber);

	}
}
