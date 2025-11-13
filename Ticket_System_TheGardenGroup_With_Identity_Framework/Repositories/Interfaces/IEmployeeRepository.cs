using MongoDB.Bson;
using Ticket_System_TheGardenGroup_With_Identity_Framework.Models;
using Ticket_System_TheGardenGroup_With_Identity_Framework.ViewModels;

namespace Ticket_System_TheGardenGroup_With_Identity_Framework.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<List<EmployeeTicketsVm>> GetAllEmployeesWithAmountOfTicketsAsync();//EmployeesIndex
        void AddEmployeeAsync(Employee employee);//AddEmployee
        void UpdateEmployeeViewModelAsync(EmployeeViewModel employeeViewModel);
        void UpdateEmployeeAsync(Employee employee);
        Task<Employee> GetEmployeeByEmployeeIdAsync(ObjectId employeeID);
        Task<Employee> GetEmployeeByEmployeeNumberAsync(int employeeNumber);
        Task DeleteEmployeeAsync(ObjectId employeeId);
        Task RemoveRegularEmployeeAsync(Employee employee);//Not currently used
        Task RemoveServiceDeskEmployeeAsync(Employee employee);//Not currently used
        Task<EmbeddedEmployee> GetActiveEmbeddedSdEmployeeByIdAsync(int employeeNumber);//ViewTicket, UpdateTicket: Not currently used

    }
}
