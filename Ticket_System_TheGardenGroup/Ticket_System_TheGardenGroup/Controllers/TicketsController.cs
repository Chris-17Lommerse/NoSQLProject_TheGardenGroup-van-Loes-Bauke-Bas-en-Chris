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

        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
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

                throw new Exception("No tickets found");
            }
            
        }
        [HttpGet]
        public ActionResult ViewTicket(ObjectId ticketID)
        {
            try
            {
                return View(_ticketService.GetTicketAsync(ticketID));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "The ViewTicket page could not be loaded.";
                return RedirectToAction("Index");
            }
        }
		/*[HttpGet]
        public async Task<IActionResult> UpdateTicket(ObjectId objId)
        {
            try
            {
                Ticket ticket = await _ticketService.GetTicketByObjIdAsync(objId);

				if (ticket == null)
				{
					TempData["NoTicket"] = "Ticket not found with id.";
					return RedirectToAction("Index");
				}

				//ticket.TicketId = ToString(objId);
				//ticket.TicketStatus = Models.Enums.TicketStatus.Open;
				//ticket.IsSolved = false;
				//ticket.ReportingEmployee = ticket.ReportingEmployee;
				//ticket.SolvingEmployee = ticket.SolvingEmployee;

				return View(ticket);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "The UpdateTicket page could not be loaded.";
                return RedirectToAction("ViewTicket");
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
        public IActionResult AddTicket(TicketViewModel ticketViewModel)
        {
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
    }
}
