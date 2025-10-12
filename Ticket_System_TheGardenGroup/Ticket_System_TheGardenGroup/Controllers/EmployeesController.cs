using Microsoft.AspNetCore.Mvc;
using Ticket_System_TheGardenGroup.Models;
using Ticket_System_TheGardenGroup.Services.Interfaces;
using Ticket_System_TheGardenGroup.ViewModels;

namespace Ticket_System_TheGardenGroup.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        public IActionResult Index()
        {
            Task<List<EmployeeTicketsVm>> employees = _employeeService.GetAllEmployeesWithAmountOfTicketsAsync();
            return View(employees);
        }
        public IActionResult ViewEmployee(string employeeID)
        {
            Employee employee = new Employee();
            //Task<List<EmployeeViewModel>> employees = _employeeService.GetEmployeeAsync();
            return View(employee);
        }
    }
}
