using MongoDB.Bson;
using Ticket_System_TheGardenGroup_With_Identity_Framework.Models;
using Ticket_System_TheGardenGroup_With_Identity_Framework.ViewModels;

namespace Ticket_System_TheGardenGroup_With_Identity_Framework.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<EmployeeTicketsVm>> GetAllEmployeesWithAmountOfTicketsAsync();
        Task<EmbeddedEmployee> GetEmbeddedEmployeeByEmployeeNumberAsync(int employeeNumber);
        Task<Employee> GetEmployeeByEmployeeIdStringAsync(string employeeIdString);
        void AddEmployee(Employee employee);
        void UpdateEmployeeViewModel(EmployeeViewModel employeeViewModel);
        void UpdateEmployee(Employee employee);
        Task<Employee> GetEmployeeByNumberAsync(string employeeNumber);
        Task DeleteEmployeeAsync(ObjectId employeeId);

        //for finding a service desk employee to embed
        Task<EmbeddedEmployee> GetActiveEmbeddedSdEmployeeByIdAsync(int employeeNumber);

    }
}
