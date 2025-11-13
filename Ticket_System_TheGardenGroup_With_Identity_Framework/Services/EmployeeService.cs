using MongoDB.Bson;
using MongoDB.Driver;
using System.Net.Sockets;
using Ticket_System_TheGardenGroup_With_Identity_Framework.Models;
using Ticket_System_TheGardenGroup_With_Identity_Framework.Models.Enums;
using Ticket_System_TheGardenGroup_With_Identity_Framework.Repositories;
using Ticket_System_TheGardenGroup_With_Identity_Framework.Repositories.Interfaces;
using Ticket_System_TheGardenGroup_With_Identity_Framework.Services.Interfaces;
using Ticket_System_TheGardenGroup_With_Identity_Framework.ViewModels;

namespace Ticket_System_TheGardenGroup_With_Identity_Framework.Services
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
        public async Task DeleteEmployeeAsync(ObjectId employeeId)
        {
            await _employeeRepository.DeleteEmployeeAsync(employeeId);
        }
        public void AddEmployee(Employee employee)
        {
            _employeeRepository.AddEmployeeAsync(employee);
        }
        public async Task<Employee> GetEmployeeByEmployeeIdStringAsync(string employeeIdString)
        {
            ObjectId employeeId = new ObjectId(employeeIdString);
            Employee employee = await _employeeRepository.GetEmployeeByEmployeeIdAsync(employeeId);
            return employee;
        }
        public async Task<Employee> GetEmployeeByEmployeeNumberAsync(int employeeNumber)
        {
            Employee employee = await _employeeRepository.GetEmployeeByEmployeeNumberAsync(employeeNumber);
            return employee;
        }
        public async Task <EmbeddedEmployee> GetEmbeddedEmployeeByEmployeeNumberAsync(int employeeNumber) 
        {
            Employee employee = await _employeeRepository.GetEmployeeByEmployeeNumberAsync(employeeNumber);
            return new EmbeddedEmployee(employee.EmployeeNumber, employee.EmployeeRole, employee.EmailAddress, employee.Name);
        }
        public async Task<Employee> GetEmployeeByNumberAsync(string employeeNumber)
        {
            if (!int.TryParse(employeeNumber, out int empNum))
                return null;

            return await _employeeRepository.GetEmployeeByEmployeeNumberAsync(empNum);
        }

        public async Task<EmbeddedEmployee> GetActiveEmbeddedSdEmployeeByIdAsync(int employeeNumber)
        {
            return await _employeeRepository.GetActiveEmbeddedSdEmployeeByIdAsync(employeeNumber);
        }

        public void UpdateEmployeeViewModel(EmployeeViewModel employeeViewModel)
        {
            _employeeRepository.UpdateEmployeeViewModelAsync(employeeViewModel);
        }
        public void UpdateEmployee(Employee employee)
        {
            _employeeRepository.UpdateEmployeeAsync(employee);
        }
    }
}
