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
                catch (Exception)
                {
                    ViewBag.ErrorMessage = " User could not be created :((((";
                    return View("AddEmployee");
                }
            }

        [HttpGet]
        public async Task<IActionResult> DeleteTicket(Employee employee)
        {
            try
            {
                //Get ticket by its id and put that in a ObjectId
                //getting the employee form the db

                //return the view with the employee obj
                return View();
            }
            catch (Exception)
            {
                TempData["WarningMessage"] = "Something went wrong";
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public async Task<IActionResult> DeleteTicket(ObjectId employeeId)
        {
            try
            {
                await _employeeService.DeleteEmployeeAsync(employeeId);
                TempData["SuccesMessage"] = $"Employee: {employeeId} is deleted";

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["WarningMessage"] = "Something went wrong";
                return RedirectToAction("Index");
            }
        }




    }

}

