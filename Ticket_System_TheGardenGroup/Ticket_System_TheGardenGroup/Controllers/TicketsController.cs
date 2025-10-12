using Microsoft.AspNetCore.Mvc;
using Ticket_System_TheGardenGroup.Models;
using Ticket_System_TheGardenGroup.Services.Interfaces;

namespace Ticket_System_TheGardenGroup.Controllers
{
    public class TicketsController : Controller
    {
        private readonly ITicketService _ticketService;

        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }
        public IActionResult Index()
        {
            Task<List<Ticket>> tickets = _ticketService.GetAllTickets();
            return View(tickets);
        }
        public IActionResult UpdateTicket()
        {
            try
            {
                throw new NotImplementedException();
                return View();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "The UpdateTicket page could not be loaded.";
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult AddTicket()
        {
            return View();
        }
    }
}
