using MongoDB.Bson;
using System.Net.Mail;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Xml.Linq;
using Ticket_System_TheGardenGroup.Models;
using Ticket_System_TheGardenGroup.Models.Enums;
using Ticket_System_TheGardenGroup.Repositories;
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
        public Task<List<Employee>> GetAllEmployees()
        {
            return _employeeRepository.GetAllEmployees();
        }



        public void AddEmployee(Employee employee)
        {
            _employeeRepository.AddEmployee(employee);
        }

        public Task<Employee> GetEmployeeByObjIdAsync(ObjectId id)
        {
            return  _employeeRepository.GetTicketByObjIdAsync(id);
        }

        public void UpdateEmployee(Employee employee)
        {
            _employeeRepository.UpdateEmployee(employee);
        }
    }
}
