using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Ticket_System_TheGardenGroup_With_Identity_Framework.Models;
using Ticket_System_TheGardenGroup_With_Identity_Framework.Models.Enums;
using Ticket_System_TheGardenGroup_With_Identity_Framework.Services.Interfaces;
using Ticket_System_TheGardenGroup_With_Identity_Framework.ViewModels;

namespace Ticket_System_TheGardenGroup.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public EmployeesController(IEmployeeService employeeService, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _employeeService = employeeService;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        [Authorize(Roles = "Service_Desk_Employee")]
        public IActionResult Index()
        {
            if (!_signInManager.IsSignedIn(User))
            {
                TempData["ErrorMessage"] = $"Je moet inloggen om toegang te krijgen tot de pagina";
                return RedirectToAction("Index", "Home");
            }
            Task<List<EmployeeTicketsVm>> employees = _employeeService.GetAllEmployeesWithAmountOfTicketsAsync();
            return View(employees);
        }

        [HttpGet]
        [Authorize(Roles = "Service_Desk_Employee")]
        public ActionResult ViewEmployee(int employeeNumber)
        {
            if (!_signInManager.IsSignedIn(User))
            {
                TempData["ErrorMessage"] = $"Je moet inloggen om toegang te krijgen tot de pagina";
                return RedirectToAction("Index", "Home");
            }
            return View(_employeeService.GetEmployeeAsync(employeeNumber));

        }
        [Authorize(Roles = "Service_Desk_Employee")]
        public IActionResult UpdateEmployee(EmployeeTicketsVm employeeTicketsVm)
        {
            if (!_signInManager.IsSignedIn(User))
            {
                TempData["ErrorMessage"] = $"Je moet inloggen om toegang te krijgen tot de pagina";
                return RedirectToAction("Index", "Home");
            }
            return View(employeeTicketsVm);
        }
        [HttpGet]
        [Authorize(Roles = "Service_Desk_Employee")]
        public ActionResult AddEmployee()
        {
            if (!_signInManager.IsSignedIn(User))
            {
                TempData["ErrorMessage"] = $"Je moet inloggen om toegang te krijgen tot de pagina";
                return RedirectToAction("Index", "Home");
            }
            return View();

        }

        [HttpPost]
        [Authorize(Roles = "Service_Desk_Employee")]
        public ActionResult AddEmployee(Employee employee)
        {
            try
            {
                if (!_signInManager.IsSignedIn(User))
                {
                    TempData["ErrorMessage"] = $"Je moet inloggen om toegang te krijgen tot de pagina";
                    return RedirectToAction("Index", "Home");
                }
                _employeeService.AddEmployee(employee);
                TempData["SuccessMessage"] = " User created successfully! :) ";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = " User could not be created.";
                return View("AddEmployee");
            }
        }

        public async Task<IActionResult> AssignRole(string userId, EmployeeRole employeeRole)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);

            await _userManager.AddToRoleAsync(user, employeeRole.ToString());

            TempData["SuccessMessage"] = $"Rol {employeeRole} is succesvol gekoppeld aan {user.UserName}";
            return RedirectToAction("Index");

        }
    }
}


