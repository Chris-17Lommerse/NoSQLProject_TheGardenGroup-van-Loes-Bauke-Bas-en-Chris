using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
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
        [HttpGet]
        public ActionResult ViewEmployee(int employeeNumber)
        {
            return View(_employeeService.GetEmployeeAsync(employeeNumber));
        }
        public IActionResult UpdateEmployee(EmployeeTicketsVm employeeTicketsVm)
        {
            return View(employeeTicketsVm);
        }
    }
}
