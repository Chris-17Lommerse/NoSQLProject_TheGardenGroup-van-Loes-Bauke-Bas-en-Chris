using Microsoft.AspNetCore.Mvc;
using Ticket_System_TheGardenGroup.Models;
using Ticket_System_TheGardenGroup.Services.Interfaces;

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
            Task<List<Employee>> employees = _employeeService.GetAllEmployees();
            return View(employees);
        }
    }
}
