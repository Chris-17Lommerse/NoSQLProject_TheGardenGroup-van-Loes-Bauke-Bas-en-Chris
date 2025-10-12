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
            Task<List<EmployeeTicketsViewModel>> employees = _employeeService.GetAllEmployeesWithAmountOfTicketsAsync();
            return View(employees);
        }
        [HttpGet]
        public ActionResult AddEmployee()
        {
            return View();

        }
        [HttpPost]
        //Blah blah blah
        public ActionResult AddEmployee(Employee employee)
        {
            try
            {
                 _employeeService.AddEmployee(employee);
                TempData["SuccessMessage"] = " User created successfully! :) ";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = " User could not be created :((((" 
                return View("AddEmployee");
            }
        }
    }
}
