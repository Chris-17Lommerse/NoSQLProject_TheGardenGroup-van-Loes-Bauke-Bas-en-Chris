using MongoDB.Bson;
using Ticket_System_TheGardenGroup_With_Identity_Framework.Models;
using Ticket_System_TheGardenGroup_With_Identity_Framework.ViewModels;

namespace Ticket_System_TheGardenGroup_With_Identity_Framework.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<List<EmployeeTicketsVm>> GetAllEmployeesWithAmountOfTicketsAsync();
        void AddEmployeeAsync(Employee employee);
        Task<Employee> GetEmployeeByEmployeeIdAsync(ObjectId employeeID);
        Task<Employee> GetEmployeeByEmployeeNumberAsync(int employeeNumber);
        Task RemoveRegularEmployeeAsync(Employee employee);
        Task RemoveServiceDeskEmployeeAsync(Employee employee);
        Task UpdateEmployeeAsync(Employee employee);
        Task<EmbeddedEmployee> GetActiveEmbeddedSdEmployeeByIdAsync(int employeeNumber);

    }
}
