using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
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

        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
				var tickets = await _ticketService.GetAllTickets()
				return View(tickets);
			}
            catch (Exception)
            {
                TempData["ErrorMessage"] = "No tickets found";
                return RedirectToAction("Index", "Home");
            }
            
        }
        [HttpGet]
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
        }
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
		[HttpPost]
        public IActionResult UpdateTicket(Ticket ticket)
        {
            try
            {
                _ticketService.UpdateTicket(ticket);
                TempData["SuccesMessage"] = "The ticket was succesfully updated.";
                return View();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "The UpdateTicket page could not be loaded.";
                return RedirectToAction("ViewTicket");
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
    }
}
