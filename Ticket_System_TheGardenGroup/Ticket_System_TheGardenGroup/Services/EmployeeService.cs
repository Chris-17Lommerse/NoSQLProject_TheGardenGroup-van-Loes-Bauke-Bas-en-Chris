using MongoDB.Bson;
using Ticket_System_TheGardenGroup.Models;
using Ticket_System_TheGardenGroup.Repositories.Interfaces;
using Ticket_System_TheGardenGroup.Services.Interfaces;
using Ticket_System_TheGardenGroup.ViewModels;

namespace Ticket_System_TheGardenGroup.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public Task<List<EmployeeTicketsVm>> GetAllEmployeesWithAmountOfTicketsAsync()
        {
            return _employeeRepository.GetAllEmployeesWithAmountOfTicketsAsync();
        }
        public Task<Employee> GetEmployeeAsync(ObjectId id)
        {
            return _employeeRepository.GetEmployeeAsync(id);
        }
        public EmployeeViewModel viewModelCasting(Task<Employee> employee)
        {
            EmployeeViewModel employeeViewModel = new EmployeeViewModel();
            
            return employeeViewModel;
        }
    }
}
