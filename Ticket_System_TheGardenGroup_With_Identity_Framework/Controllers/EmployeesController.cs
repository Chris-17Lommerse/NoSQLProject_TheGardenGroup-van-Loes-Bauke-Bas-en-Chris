using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using Ticket_System_TheGardenGroup_With_Identity_Framework.Models;
using Ticket_System_TheGardenGroup_With_Identity_Framework.Services.Interfaces;
using Ticket_System_TheGardenGroup_With_Identity_Framework.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;


namespace Ticket_System_TheGardenGroup.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly SignInManager<IdentityUser> _signInManager;

        public EmployeesController(IEmployeeService employeeService, SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
            _employeeService = employeeService;
        }

        public IActionResult Index()
        {
            if (!_signInManager.IsSignedIn(User))
            {
                TempData["ErrorMessage"] = $"Je moet inloggen om toegang te hebben tot deze pagina";
                return RedirectToAction("Index", "Home");
            }

            Task<List<EmployeeTicketsVm>> employees = _employeeService.GetAllEmployeesWithAmountOfTicketsAsync();
                return View(employees);
        }

        [HttpGet]
        public ActionResult ViewEmployee(int employeeNumber)
        {
            if (!_signInManager.IsSignedIn(User))
            {
                TempData["ErrorMessage"] = $"Je moet inloggen om toegang te hebben tot deze pagina";
                return RedirectToAction("Index", "Home");
            }
            return View(_employeeService.GetEmployeeAsync(employeeNumber));

        }
        public IActionResult UpdateEmployee(EmployeeTicketsVm employeeTicketsVm)
        {
            if (!_signInManager.IsSignedIn(User))
            {
                TempData["ErrorMessage"] = $"Je moet inloggen om toegang te hebben tot deze pagina";
                return RedirectToAction("Index", "Home");
            }
            return View(employeeTicketsVm);
        }
        [HttpGet]
        public ActionResult AddEmployee()
        {
            if (!_signInManager.IsSignedIn(User))
            {
                TempData["ErrorMessage"] = $"Je moet inloggen om toegang te hebben tot deze pagina";
                return RedirectToAction("Index", "Home");
            }
            return View();

        }

        [HttpPost]
        //Blah blah blah
        public ActionResult AddEmployee(Employee employee)
        {
            try
            {
                if (!_signInManager.IsSignedIn(User))
                {
                    TempData["ErrorMessage"] = $"Je moet inloggen om toegang te hebben tot deze pagina";
                    return RedirectToAction("Index", "Home");
                }
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


