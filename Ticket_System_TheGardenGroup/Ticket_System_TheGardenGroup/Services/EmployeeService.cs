using Ticket_System_TheGardenGroup.Models;
using Ticket_System_TheGardenGroup.Repositories.Interfaces;
using Ticket_System_TheGardenGroup.Services.Interfaces;

namespace Ticket_System_TheGardenGroup.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public Task<List<Employee>> GetAllEmployees()
        {
            return _employeeRepository.GetAllEmployees();
        }
    }
}
