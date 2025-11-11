using MongoDB.Bson;
using Ticket_System_TheGardenGroup_With_Identity_Framework.Models;
using Ticket_System_TheGardenGroup_With_Identity_Framework.ViewModels;

namespace Ticket_System_TheGardenGroup_With_Identity_Framework.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<List<EmployeeTicketsVm>> GetAllEmployeesWithAmountOfTicketsAsync();
        void AddEmployee(Employee employee);
        Task<Employee> GetEmployeeByEmployeeIdAsync(ObjectId employeeID);
        Task<Employee> GetEmployeeByEmployeeNumberAsync(int employeeNumber);
        Task RemoveRegularEmployee(Employee employee);
        Task RemoveServiceDeskEmployee(Employee employee);
        Task UpdateEmployee(Employee employee);
        Task<EmbeddedEmployee> GetActiveEmbeddedSdEmployeeByIdAsync(int employeeNumber);

    }
}
