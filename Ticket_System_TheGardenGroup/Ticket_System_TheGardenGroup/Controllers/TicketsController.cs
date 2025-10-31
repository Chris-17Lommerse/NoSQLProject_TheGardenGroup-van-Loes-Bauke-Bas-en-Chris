using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Linq.Expressions;
using Ticket_System_TheGardenGroup.Models;
using Ticket_System_TheGardenGroup.Services;
using Ticket_System_TheGardenGroup.Services.Interfaces;
using Ticket_System_TheGardenGroup.ViewModels;

namespace Ticket_System_TheGardenGroup.Controllers
{
    public class TicketsController : Controller
    {
        private readonly ITicketService _ticketService;
        private readonly IEmployeeService _employeeService;

        public TicketsController(ITicketService ticketService, IEmployeeService employeeService)
        {
            _ticketService = ticketService;
            _employeeService = employeeService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
				var tickets = await _ticketService.GetAllTickets()
				return View(tickets);
			}
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "No tickets found";
                return RedirectToAction("Index", "Home");
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
		public async Task<IActionResult> UpdateTicket(string ticketId)
		{
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
        public async Task<IActionResult> UpdateTicket(Ticket ticket, int employeeNumber, string loadEmployee)
        {
            //This needs to be reworked. Either I make a new view model or something with JavaScript. 
            try
            {
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
                else {
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
        public IActionResult AddTicket()
        {
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
        [HttpPost]
        public IActionResult AddTicket(TicketViewModel ticketViewModel)
        {
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
        public async Task<IActionResult> DeleteTicket(ObjectId ticketId)
        {
            try
            {
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
		public async Task<IActionResult> DeleteTicket()
		{
            throw new NotImplementedException();
		}

	}
}
