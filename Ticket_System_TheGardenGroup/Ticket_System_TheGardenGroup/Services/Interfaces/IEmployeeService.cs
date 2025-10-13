using MongoDB.Bson;
using Ticket_System_TheGardenGroup.Models;
using Ticket_System_TheGardenGroup.ViewModels;

namespace Ticket_System_TheGardenGroup.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<EmployeeTicketsVm>> GetAllEmployeesWithAmountOfTicketsAsync();
        Task<List<Employee>> GetAllEmployees();
        Task<Employee> GetEmployeeByObjIdAsync(ObjectId id);
        void AddEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
    }
}
