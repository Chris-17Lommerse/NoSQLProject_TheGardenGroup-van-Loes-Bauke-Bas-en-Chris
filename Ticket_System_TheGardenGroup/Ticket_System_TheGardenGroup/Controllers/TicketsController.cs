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
                var tickets = await _ticketService.GetAllTickets();
				return View(tickets);
			}
            catch (Exception)
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
            try
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
                TempData["SuccesMessage"] = "The ticket was succesfully updated.";
                return View(ticket);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "The ticket has failed to update.";
                return RedirectToAction("UpdateTicket");
            }
		}
        [HttpGet]
        public async Task<IActionResult> LoadEmployee(Ticket ticket, int employeeNumber)
        {
            try
            {
                EmbeddedEmployee embeddedEmployee = await _employeeService.GetActiveEmbeddedSdEmployeeByIdAsync(employeeNumber);
                if (embeddedEmployee == null)
                {
                    TempData["ErrorMessage"] = "Empty embedded employee";
                    return RedirectToAction("UpdateTicket");
                }
                ticket.SolvingEmployee = embeddedEmployee;

                return View(ticket);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult UpdateTicket(Ticket ticket)
        {
            //This needs to be reworked. Either I make a new view model or something with JavaScript. 
            try
            {
                //Sends the new info to the DB
                _ticketService.UpdateTicket(ticket);
                TempData["SuccesMessage"] = "The ticket was succesfully updated.";
                return View(ticket);
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
                TempData["SuccesMessage"] = "The ticket was succesfully created.";
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
		public IActionResult DeleteTicket(Ticket ticket)
		{
            try
            {
                //first delete the ticket from workingOn array
                //

                //This deletes a ticket
                _ticketService.DeleteTicket(ticket);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return RedirectToAction("Index");
            }
		}

	}
}
