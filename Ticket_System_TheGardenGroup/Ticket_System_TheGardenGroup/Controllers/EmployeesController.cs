using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Collections.Generic;
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
        public async Task<IActionResult> Index()
        {
            List<Employee> employees = await _employeeService.GetAllActiveEmployees();
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
                    ViewBag.ErrorMessage = " User could not be created :((((";
                    return View("AddEmployee");
                }
            }

        /*[HttpGet]
        public async Task<IActionResult> DeleteTicket(string employeeId)
        {
            try
            {
                
            }
            catch (Exception)
            {

            }
        }
        [HttpPost]
        public IActionResult DeleteTicket(Ticket ticket)
        {
            try
            {

            }
            catch (Exception)
            {

            }
        }*/




    }

}

