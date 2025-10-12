using MongoDB.Bson;
using System.Net.Mail;
using System.Xml.Linq;
using Ticket_System_TheGardenGroup.Models;
using Ticket_System_TheGardenGroup.Models.Enums;
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
        public async Task<EmployeeViewModel> GetEmployeeAsync(int employeeNumber)
        {
            Employee employee = await _employeeRepository.GetEmployeeAsync(employeeNumber);
            if (employee == null) { return new EmployeeViewModel(employeeNumber, "emailAddress", "name", "surname", EmployeeRole.REGULAR_EMPLOYEE, false); }
            EmployeeViewModel employeeViewModel = new EmployeeViewModel(employee);
            return employeeViewModel;
        }
    }
}
