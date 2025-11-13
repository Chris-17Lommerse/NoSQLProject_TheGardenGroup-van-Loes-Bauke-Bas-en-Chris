using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Net.Sockets;
using Ticket_System_TheGardenGroup_With_Identity_Framework.Models;
using Ticket_System_TheGardenGroup_With_Identity_Framework.Models.Enums;
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
                if(!_signInManager.IsSignedIn(User))
                {
                    TempData["ErrorMessage"] = $"Je moet inloggen om toegang te krijgen tot de pagina";
                    return RedirectToAction("Index", "Home");
                }
                List<Ticket> tickets = await _ticketService.GetAllTickets();

                return View(tickets);
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

                if(tickets.Count == 0)
                {
                    TempData["ErrorMessage"] = $"Er konden geen tickets worden gevonden";
                    return View(tickets);
                }
                return View(tickets);
            } catch (ArgumentNullException ex)
            {
                TempData["ErrorMessage"] = $"Kon geen tickets vinden. {ex.Message}";
                return View(ex);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Er is iets misgegaan. {ex.Message}";
                return View(ex);
            }
        }
        [HttpGet]
        [Authorize(Roles = "Service_Desk_Employee,Regular_Employee")]
        public async Task<IActionResult> ViewTicket(string ticketIdString)
        {
            try
            {
                if (!_signInManager.IsSignedIn(User))
                {
                    TempData["ErrorMessage"] = $"Je moet inloggen om toegang te krijgen tot de pagina";
                    return RedirectToAction("Index", "Home");
                }
                ObjectId ticketId = new ObjectId(ticketIdString);
                Ticket ticket = await _ticketService.GetTicketByObjIdAsync(ticketId);
                return View(ticket);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "The ViewTicket page could not be loaded.";
                return RedirectToAction("Index");
            }
        }
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

            ObjectId objectId = new ObjectId(ticketId);
            Ticket ticket = await _ticketService.GetTicketByObjIdAsync(objectId);

            if (ticket == null)
            {
                TempData["NoTicket"] = "Ticket not found.";
                return RedirectToAction("Index");
            }

            return View(ticket);
        }
        [HttpPost]
        [Authorize(Roles = "Service_Desk_Employee")]
        public async Task<IActionResult> UpdateTicket(string ticketId, int solvingEmployeeNumber)
        {
            try
            {
                EmbeddedEmployee embeddedSolvingEmployee = await _employeeService.GetEmbeddedEmployeeByEmployeeNumberAsync(solvingEmployeeNumber);
                Console.WriteLine(embeddedSolvingEmployee.ToString());
                ObjectId objectId = new ObjectId(ticketId);
                Ticket ticket = await _ticketService.GetTicketByObjIdAsync(objectId);
                ticket.SolvingEmployee = embeddedSolvingEmployee;
                Console.WriteLine(ticket.SolvingEmployee.ToString());
                TempData["SuccesMessage"] = "The update embeddedEmployee succeeded.";
                return View(ticket);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(Environment.NewLine + ex);
                TempData["ErrorMessage"] = "The update embeddedEmployee failed.";
                return RedirectToAction("Index", "Tickets");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Service_Desk_Employee")]
        public async Task<IActionResult> UpdateTicketSuccess(Ticket ticket)
        {
            try
            {
                if (!_signInManager.IsSignedIn(User))
                {
                    TempData["ErrorMessage"] = "Je moet inloggen om toegang te krijgen tot de pagina.";
                    return RedirectToAction("Index", "Home");
                }
                _ticketService.UpdateTicket(ticket);
                Ticket updatedTicket = await _ticketService.GetTicketByObjIdAsync(ticket.TicketId);
                if (await _ticketService.CheckUpdatedTicket(ticket, updatedTicket))
                {
                    TempData["SuccesMessage"] = "The ticket was succesfully updated.";
                    return View(updatedTicket);
                }
                else 
                {
                    TempData["ErrorMessage"] = "The ticket failed to update.";
                    return View(ticket);
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "The HttpPost UpdateTicket page failed to load.";
                return RedirectToAction("UpdateTicket", ticket);
            }
        }
        
        [HttpGet]
        [Authorize(Roles = "Service_Desk_Employee")]
        public async Task<IActionResult> UpdateTicketSuccess()
        {
            try
            {
                TempData["SuccesMessage"] = "The ticket was updated successfully.";
                return View();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "UpdateTicketSuccess failed to load. However your ticket was updated correctly";
                return RedirectToAction("Index", "Tickets");
            }
        }
        
        [HttpGet]
        [Authorize(Roles = "Service_Desk_Employee,Regular_Employee")]
        public IActionResult AddTicket()
        {
            try
            {
                if (!_signInManager.IsSignedIn(User))
                {
                    TempData["ErrorMessage"] = $"Je moet inloggen om toegang te krijgen tot de pagina";
                    return RedirectToAction("Index", "Home");
                }
                throw new NotImplementedException();
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
            try 
            {
                if (!_signInManager.IsSignedIn(User))
                {
                    TempData["ErrorMessage"] = $"Je moet inloggen om toegang te krijgen tot de pagina";
                    return RedirectToAction("Index", "Home");
                }
                throw new NotImplementedException();
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