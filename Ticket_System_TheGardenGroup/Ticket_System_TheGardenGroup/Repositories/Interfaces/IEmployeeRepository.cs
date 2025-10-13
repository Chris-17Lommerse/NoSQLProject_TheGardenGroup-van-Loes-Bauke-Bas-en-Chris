using MongoDB.Bson;
using Ticket_System_TheGardenGroup.Models;
using Ticket_System_TheGardenGroup.ViewModels;

namespace Ticket_System_TheGardenGroup.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<List<EmployeeTicketsVm>> GetAllEmployeesWithAmountOfTicketsAsync();
        Task<List<Employee>> GetAllEmployees();
        List<Employee> GetAllRegularEmployees();
        List<Employee> GetAllServiceDeskEmployees();
        void AddEmployee(Employee employee);
        Task<Employee> GetEmployeeAsync(ObjectId objId);
        Task<Employee> GetTicketByObjIdAsync(ObjectId objId);
        //Task RemoveRegularEmployee(Employee employee);
        //Task RemoveServiceDeskEmployee(Employee employee);
        //Task UpdateRegularEmployee(Employee employee);
        //Task UpdateServiceDeskEmployee(Employee employee);

        void UpdateEmployee(Employee employee);
    }
}
