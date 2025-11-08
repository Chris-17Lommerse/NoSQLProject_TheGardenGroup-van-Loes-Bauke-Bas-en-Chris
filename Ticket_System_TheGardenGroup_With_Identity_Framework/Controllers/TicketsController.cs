using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using MongoDB.Bson;
using Ticket_System_TheGardenGroup_With_Identity_Framework.Models;
using Ticket_System_TheGardenGroup_With_Identity_Framework.Services.Interfaces;
using Ticket_System_TheGardenGroup_With_Identity_Framework.ViewModels;

namespace Ticket_System_TheGardenGroup_With_Identity_Framework.Controllers
{
    public class TicketsController : Controller
    {
        private readonly ITicketService _ticketService;
        private readonly IEmployeeService _employeeService;
        private readonly SignInManager<IdentityUser> _signInManager;

        public TicketsController(ITicketService ticketService, IEmployeeService employeeService, SignInManager<IdentityUser> signInManager)
        {
            _ticketService = ticketService;
            _employeeService = employeeService;
            _signInManager = signInManager;
           
        }
        [HttpGet]
        [Authorize(Roles = "Service_Desk_Employee,Regular_Employee")]
        public async Task<IActionResult> Index()
        {
            try
            {
                List<Ticket> tickets;
                if(!_signInManager.IsSignedIn(User))
                {
                    TempData["ErrorMessage"] = $"Je moet inloggen om toegang te krijgen tot de pagina";
                    return RedirectToAction("Index", "Home");
                }
                if (User.IsInRole("Service_Desk_Employee"))
                {
                    tickets = await _ticketService.GetAllTickets();
                    return View(tickets);
                }
                else if(User.IsInRole("Regular_Employee"))
                {
                    tickets = await _ticketService.GetTicketsByEmployeeId();
                    return View(tickets);
                }
                else
                {
                    TempData["ErrorMessage"] = $"Je hebt geen toegang tot deze pagina";
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception)
            {
                throw new Exception("No tickets found");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Service_Desk_Employee,Regular_Employee")]
        public async Task<IActionResult> Index(string searchString)
        {
            try
            {
                List<Ticket> tickets;
                if(!string.IsNullOrEmpty(searchString))
                {
                   tickets = await _ticketService.FilterTicketsOnSearchInputAsync(searchString);
                   
                }
                else
                {
                   tickets = await _ticketService.GetAllTickets();
                }

                if(tickets.Count > 0)
                {
                    TempData["SuccesMessage"] = $"Er zijn {tickets.Count} tickets gevonden die overeenkomen met de invoer";
                    return View(tickets);
                }

                TempData["ErrorMessage"] = $"Er konden geen tickets worden gevonden";
                return View(tickets);
            } catch (ArgumentNullException ex)
            {
                TempData["ErrorMessage"] = $"Kon geen tickets vinden. {ex.Message}";
                return View(ex);
            }
            catch (Exception ex)
            {
                TempData["EroorMessage"] = $"Er is iets misgegaan. {ex.Message}";
                return View(ex);
            }
        }
        /*[HttpGet]
        public ActionResult ViewTicket(ObjectId employeeID)
        {
            try
            {
                return View(_ticketService.GetTicketAsync(employeeID));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "The ViewTicket page could not be loaded.";
                return RedirectToAction("Index");
            }
        }*/
        [HttpGet]
        [Authorize(Roles = "Service_Desk_Employee")]
        public async Task<IActionResult> UpdateTicket(string ticketId)
        {
            if (!_signInManager.IsSignedIn(User))
            {
                TempData["ErrorMessage"] = $"Je moet inloggen om toegang te krijgen tot de pagina";
                return RedirectToAction("Index", "Home");
            }

            if (string.IsNullOrEmpty(ticketId))
            {
                TempData["NoId"] = "Ticket ID missing.";
                return RedirectToAction("Index");
            }

            var objectId = new ObjectId(ticketId);
            var ticket = await _ticketService.GetTicketByObjIdAsync(objectId);

            if (ticket == null)
            {
                TempData["NoTicket"] = "Ticket not found.";
                return RedirectToAction("Index");
            }

            return View(ticket);
        }
        /*[HttpGet]
        public async Task<IActionResult> UpdateEmbeddedEmployee(int employeeNumber)
        {
            //I am working on this, but it's probably not gonna work. 
            try
            {
                //Find the service desk employee for embedding
                var embeddedEmployee = await _employeeService.GetEmbeddedSdEmployeeByIdAsync(employeeNumber);

                if (embeddedEmployee == null)
                {
                    TempData["EmbddEmpl"] = "This employee is not found.";
                }
                ViewBag.EmbddEmployee = embeddedEmployee;

                return View(embeddedEmployee);
            }
            catch (Exception ex)
            {
                throw new Exception("Error message:" + ex);
            }
        }*/

        [HttpPost]
        [Authorize(Roles = "Service_Desk_Employee")]
        public async Task<IActionResult> UpdateTicket(Ticket ticket, int employeeNumber, string loadEmployee)
        {
            //This needs to be reworked. Either I make a new view model or something with JavaScript. 
            try
            {
                if (!_signInManager.IsSignedIn(User))
                {
                    TempData["ErrorMessage"] = $"Je moet inloggen om toegang te krijgen tot de pagina";
                    return RedirectToAction("Index", "Home");
                }
                //This if statement is not working :0
                if (!string.IsNullOrEmpty(loadEmployee))
                {
                    // Load employee
                    var embddEmpl = await _employeeService.GetActiveEmbeddedSdEmployeeByIdAsync(employeeNumber);
                    if (embddEmpl != null)
                    {
                        ticket.SolvingEmployee = embddEmpl;
                    }
                    else
                    {
                        TempData["EmbddEmpl"] = "EmbeddedEmployee not found.";
                    }

                    return View(ticket); // reload the form with updated employee
                }
                else
                {
                    //Sends the new info to the DB
                    _ticketService.UpdateTicket(ticket);
                    TempData["SuccesMessage"] = "The ticket was succesfully updated.";
                    return View(ticket);
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "The UpdateTicket page could not be loaded.";
                return RedirectToAction("UpdateTicket", ticket);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Service_Desk_Employee,Regular_Employee")]
        public IActionResult AddTicket()
        {
            if (!_signInManager.IsSignedIn(User))
            {
                TempData["ErrorMessage"] = $"Je moet inloggen om toegang te krijgen tot de pagina";
                return RedirectToAction("Index", "Home");
            }
            throw new NotImplementedException();
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "The AddTicket page could not be loaded.";
                return RedirectToAction("ViewTicket");
            }
        }
        [HttpGet]
        [Authorize(Roles = "Service_Desk_Employee,Regular_Employee")]
        public IActionResult AddTicket(TicketViewModel ticketViewModel)
        {
            if (!_signInManager.IsSignedIn(User))
            {
                TempData["ErrorMessage"] = $"Je moet inloggen om toegang te krijgen tot de pagina";
                return RedirectToAction("Index", "Home");
            }
            throw new NotImplementedException();
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "The AddTicket page could not be loaded.";
                return RedirectToAction("ViewTicket");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Service_Desk_Employee")]
        public async Task<IActionResult> DeleteTicket(ObjectId ticketId)
        {
            try
            {
                if (!_signInManager.IsSignedIn(User))
                {
                    TempData["ErrorMessage"] = $"Je moet inloggen om toegang te krijgen tot de pagina";
                    return RedirectToAction("Index", "Home");
                }
                /*if (string.IsNullOrEmpty(ticketId))
				{
					TempData["NoId"] = "Ticket ID missing.";
					return RedirectToAction("Index");
				}*/

                //var objectId = new ObjectId(ticketId);
                var ticket = await _ticketService.GetTicketByObjIdAsync(ticketId);

                if (ticket == null)
                {
                    TempData["NoTicket"] = "Ticket not found.";
                    return RedirectToAction("Index");
                }

                return View(ticket);
            }
            catch (Exception)
            {
                throw new Exception("No ticket found to delete");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Service_Desk_Employee")]
        public async Task<IActionResult> DeleteTicket()
        {
            if (!_signInManager.IsSignedIn(User))
            {
                TempData["ErrorMessage"] = $"Je moet inloggen om toegang te krijgen tot de pagina";
                return RedirectToAction("Index", "Home");
            }
            throw new NotImplementedException();
        }
    }
}
