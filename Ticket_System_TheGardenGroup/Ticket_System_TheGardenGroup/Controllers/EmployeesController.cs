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
            try
            {
                Task<List<EmployeeTicketsVm>> employees = _employeeService.GetAllEmployeesWithAmountOfTicketsAsync();
                return View(employees);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "The Index page of employeescould not be loaded.";
                return RedirectToAction("ViewEmployee");
            }
        }

        [HttpGet]
        public ActionResult ViewEmployee(int employeeNumber)
        {
            try
            {
                return View(_employeeService.GetEmployeeAsync(employeeNumber));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "The ViewEmployee page could not be loaded.";
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public IActionResult UpdateEmployee(EmployeeTicketsVm employeeTicketsVm)
        {
            try
            {
                return View(employeeTicketsVm);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "The UpdateEmployee page could not be loaded.";
                return RedirectToAction("ViewEmployee");
            }
        }
        [HttpGet]
        public ActionResult AddEmployee()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "The AddEmployee page could not be loaded.";
                return RedirectToAction("ViewEmployee");
            }
        }
            
        [HttpPost]
            public ActionResult AddEmployee(Employee employee)
            {
                try
                {
                    _employeeService.AddEmployee(employee);
                    TempData["SuccessMessage"] = "User created successfully!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                TempData["ErrorMessage"] = "User could not be created.";
                    return View("AddEmployee");
                }
            }
        }
    }

