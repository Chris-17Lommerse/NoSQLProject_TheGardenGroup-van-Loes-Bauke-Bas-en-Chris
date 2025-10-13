using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Ticket_System_TheGardenGroup.Models;
using Ticket_System_TheGardenGroup.Services;
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
            Task<List<Employee>> employees = _employeeService.GetAllEmployees();
            return View(employees);
        }

        [HttpGet]
        public ActionResult ViewEmployee(ObjectId employeeID)
        {
            try
            {
                return View(_employeeService.GetEmployeeByObjIdAsync(employeeID));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "The ViewEmployee page could not be loaded.";
                return RedirectToAction("Index");
            }

        }
        public IActionResult UpdateEmployee(EmployeeTicketsVm employeeTicketsVm)
        {
            return View(employeeTicketsVm);
        }
        [HttpGet]
        public ActionResult AddEmployee()
        {
           return View();
        }
            
        [HttpPost]
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
                    ViewBag.ErrorMessage = " User could not be created :((((";
                    return View("AddEmployee");
                }
            }
        }
    }

