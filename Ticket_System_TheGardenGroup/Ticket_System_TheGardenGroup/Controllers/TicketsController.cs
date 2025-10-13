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
            //Task<List<Ticket>> tickets = _ticketService.GetAllTickets();

            var tickets = await _ticketService.GetAllTickets();

            return View(tickets);
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
        public async Task<IActionResult> UpdateTicket(ObjectId objId)
        {
            try
            {
                Ticket ticket = await _ticketService.GetTicketByObjIdAsync(objId);
                
                return View(ticket);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "The UpdateTicket page could not be loaded.";
                return RedirectToAction("ViewTicket");
            }
        }
        [HttpPost]
        public IActionResult UpdateTicket(TicketViewModel ticketViewModel)
        {
            throw new NotImplementedException();
            try
            {
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
