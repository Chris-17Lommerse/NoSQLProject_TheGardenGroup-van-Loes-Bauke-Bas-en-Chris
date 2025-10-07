using Ticket_System_TheGardenGroup.Models;

namespace Ticket_System_TheGardenGroup.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        List<Employee> GetAllEmployees();
        List<Employee> GetAllRegularEmployees();
        List<Employee> GetAllServiceDeskEmployees();
        void AddRegularEmployee(Employee employee);
        void AddServiceDeskEmployee(Employee employee);
        void RemoveRegularEmployee(Employee employee);
        void RemoveServiceDeskEmployee(Employee employee);
        void UpdateRegularEmployee(Employee employee);
        void UpdateServiceDeskEmployee(Employee employee);
    }
}
