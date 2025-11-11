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

            ObjectId objectId = new ObjectId(ticketId);
            Ticket ticket = await _ticketService.GetTicketByObjIdAsync(objectId);

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
        //public async Task<IActionResult> UpdateTicket(string ticketId, DateTime creationTime, string ticketStatus, string ticketName, string description, string ticketEscalationDescription, bool isSolved, string priority, int reportingEmployeeEmployeeNumber, string reportingEmployeeName, string reportingEmployeeEmailAddress, string reportingEmployeeEmployeeRole, int solvingEmployeeEmployeeNumber, string solvingEmployeeName, string solvingEmployeeEmailAddress, string solvingEmployeeEmployeeRole)
        public async Task<IActionResult> UpdateTicket(Ticket ticket)
        {
            /*Ticket ticket;
            TicketStatus ticketStatusEnum;
            switch (ticketStatus)
            {
                case "Open":
                    ticketStatusEnum = TicketStatus.Open;
                    break;
                case "Closed":
                    ticketStatusEnum = TicketStatus.Closed;
                    break;
                case "Resolved":
                    ticketStatusEnum = TicketStatus.Resolved;
                    break;
                default:
                    ticketStatusEnum = TicketStatus.Open;
                    break;
            }
            TicketPriorityEnum priorityEnum;
            switch (priority)
            {
                case "P1":
                    priorityEnum = TicketPriorityEnum.P1;
                    break;
                case "P2":
                    priorityEnum = TicketPriorityEnum.P2;
                    break;
                case "P3":
                    priorityEnum = TicketPriorityEnum.P3;
                    break;
                case "P4":
                    priorityEnum = TicketPriorityEnum.P4;
                    break;
                case "P5":
                    priorityEnum = TicketPriorityEnum.P5;
                    break;
                default:
                    priorityEnum = TicketPriorityEnum.P1;
                    break;
            }
            EmployeeRole reportingEmployeeEmployeeRoleEnum;
            switch (reportingEmployeeEmployeeRole)
            {
                case "Service_Desk_Employee":
                    reportingEmployeeEmployeeRoleEnum = EmployeeRole.Service_Desk_Employee;
                    break;
                case "Regular_Employee":
                    reportingEmployeeEmployeeRoleEnum = EmployeeRole.Regular_Employee;
                    break;
                default:
                    reportingEmployeeEmployeeRoleEnum = EmployeeRole.Regular_Employee;
                    break;
            }
            EmployeeRole solvingEmployeeEmployeeRoleEnum;
            switch (priority)
            {
                case "Service_Desk_Employee":
                    solvingEmployeeEmployeeRoleEnum = EmployeeRole.Service_Desk_Employee;
                    break;
                case "Regular_Employee":
                    solvingEmployeeEmployeeRoleEnum = EmployeeRole.Regular_Employee;
                    break;
                default:
                    solvingEmployeeEmployeeRoleEnum = EmployeeRole.Regular_Employee;
                    break;
            }
            ObjectId ticketObjectId = new ObjectId(ticketId);
            ticket = new Ticket(ticketObjectId, creationTime, ticketStatusEnum, ticketName, description, isSolved, priorityEnum, ticketEscalationDescription, new EmbeddedEmployee(reportingEmployeeEmployeeNumber, reportingEmployeeEmployeeRoleEnum, reportingEmployeeEmailAddress, reportingEmployeeName), new EmbeddedEmployee(solvingEmployeeEmployeeNumber, solvingEmployeeEmployeeRoleEnum, solvingEmployeeEmailAddress, solvingEmployeeName));
            */
            Console.WriteLine(ticket.ToString());
            try
            {
                if (!_signInManager.IsSignedIn(User))
                {
                    TempData["ErrorMessage"] = "Je moet inloggen om toegang te krijgen tot de pagina.";
                    return RedirectToAction("Index", "Home");
                }
                //Sends the new info to the DB
                _ticketService.UpdateTicket(ticket);
                Ticket updatedTicket = await _ticketService.GetTicketByObjIdAsync(ticket.TicketId);
                if (updatedTicket == null) 
                {
                    TempData["ErrorMessage"] = "The update corrupted the ticket or something else went terribly wrong.";
                    return RedirectToAction("Index", "Tickets");
                }
                else if (!Debugger(ticket, updatedTicket)) 
                {
                    TempData["ErrorMessage"] = "Ik weet niet hoe je hier bent gekomen, maar er is waarschijnlijk iets fout gegaan op de HttpPost van UpdateTicket.";
                    return RedirectToAction("Index", "Tickets");
                }
                else
                {
                    TempData["SuccesMessage"] = "The ticket was succesfully updated.";
                    return View(updatedTicket);
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "The HttpPost UpdateTicket page failed to load.";
                Console.WriteLine(ex.ToString());
                return RedirectToAction("UpdateTicket", ticket);
            }
        }
        
        public bool Debugger(Ticket ticket, Ticket updatedTicket)
        {
            bool hasTicketUpdatedCorrectly = true;
            Console.BackgroundColor = ConsoleColor.Magenta;
            Console.WriteLine();
            Console.WriteLine("Debugging UpdateTicket HttpPost");
            Console.WriteLine("ticket : updatedTicket"); //what should have happened : what actually happened
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine();
            if (ticket.TicketId != updatedTicket.TicketId)
            {
                Console.WriteLine($"TicketId as ObjectId: {ticket.TicketId} != {updatedTicket.TicketId}");
                switch (ticket.TicketId.ToString() == updatedTicket.TicketId.ToString())
                {
                    case true:
                        Console.WriteLine($"TicketId as string: {ticket.TicketId.ToString()} == {updatedTicket.TicketId.ToString()}");
                        break;
                    case false:
                        Console.WriteLine($"TicketId as string: {ticket.TicketId.ToString()} != {updatedTicket.TicketId.ToString()}");
                        break;
                }
                hasTicketUpdatedCorrectly = false;
            }
            
            if (ticket.CreationTime != updatedTicket.CreationTime)
            {
                Console.WriteLine($"CreationTime as DateTime: {ticket.CreationTime} != {updatedTicket.CreationTime}");
                Console.WriteLine($"DateTime.Compare: { DateTime.Compare(ticket.CreationTime, updatedTicket.CreationTime)}");
                switch (ticket.CreationTime.ToString() == updatedTicket.CreationTime.ToString())
                {
                    case true:
                        Console.WriteLine($"CreationTime as string: {ticket.CreationTime.ToString()} == {updatedTicket.CreationTime.ToString()}");
                        break;
                    case false:
                        Console.WriteLine($"CreationTime as string: {ticket.CreationTime} != {updatedTicket.CreationTime}");
                        break;
                }
            }
            if (ticket.TicketStatus != updatedTicket.TicketStatus) { hasTicketUpdatedCorrectly = false; Console.WriteLine($"TicketStatus: {ticket.TicketStatus} != {updatedTicket.TicketStatus}"); }
            if (ticket.TicketName != updatedTicket.TicketName) { hasTicketUpdatedCorrectly = false; Console.WriteLine($"TicketName: {ticket.TicketName} != {updatedTicket.TicketName}"); }
            if (ticket.Description != updatedTicket.Description) { hasTicketUpdatedCorrectly = false; Console.WriteLine($"Description: {ticket.Description} != {updatedTicket.Description}"); }
            if (ticket.IsSolved != updatedTicket.IsSolved) { hasTicketUpdatedCorrectly = false; Console.WriteLine($"IsSolved: {ticket.IsSolved} != {updatedTicket.IsSolved}"); }
            if (ticket.Priority != updatedTicket.Priority) { hasTicketUpdatedCorrectly = false; Console.WriteLine($"Priority: {ticket.Priority} != {updatedTicket.Priority}"); }
            if (ticket.TicketEscalationDescription != updatedTicket.TicketEscalationDescription)
            {
                Console.WriteLine($"TicketEscalationDescription: {ticket.TicketEscalationDescription} != {updatedTicket.TicketEscalationDescription}");
                hasTicketUpdatedCorrectly = false;
            }
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("ReportingEmployee:");
            if (ticket.ReportingEmployee.EmployeeNumber != updatedTicket.ReportingEmployee.EmployeeNumber) { hasTicketUpdatedCorrectly = false; Console.WriteLine($"EmployeeNumber: {ticket.ReportingEmployee.EmployeeNumber} != {updatedTicket.ReportingEmployee.EmployeeNumber}"); }
            if (ticket.ReportingEmployee.EmployeeRole != updatedTicket.ReportingEmployee.EmployeeRole) { hasTicketUpdatedCorrectly = false; Console.WriteLine($"EmployeeRole: {ticket.ReportingEmployee.EmployeeRole} != {updatedTicket.ReportingEmployee.EmployeeRole}"); }
            if (ticket.ReportingEmployee.EmailAddress != updatedTicket.ReportingEmployee.EmailAddress) { hasTicketUpdatedCorrectly = false; Console.WriteLine($"EmailAddress: {ticket.ReportingEmployee.EmailAddress} != {updatedTicket.ReportingEmployee.EmailAddress}"); }
            if (ticket.ReportingEmployee.Name != updatedTicket.ReportingEmployee.Name)
            {
                hasTicketUpdatedCorrectly = false; Console.WriteLine($"Name: {ticket.ReportingEmployee.Name} != {updatedTicket.ReportingEmployee.Name}");
            }
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine("SolvingEmployee:");
            if (ticket.SolvingEmployee.EmployeeNumber != updatedTicket.SolvingEmployee.EmployeeNumber) { hasTicketUpdatedCorrectly = false; Console.WriteLine($"EmployeeNumber: {ticket.SolvingEmployee.EmployeeNumber} != {updatedTicket.SolvingEmployee.EmployeeNumber}"); }
            if (ticket.SolvingEmployee.EmployeeRole != updatedTicket.SolvingEmployee.EmployeeRole) { hasTicketUpdatedCorrectly = false; Console.WriteLine($"EmployeeRole: {ticket.SolvingEmployee.EmployeeRole} != {updatedTicket.SolvingEmployee.EmployeeRole}"); }
            if (ticket.SolvingEmployee.EmailAddress != updatedTicket.SolvingEmployee.EmailAddress) { hasTicketUpdatedCorrectly = false; Console.WriteLine($"EmailAddress: {ticket.SolvingEmployee.EmailAddress} != {updatedTicket.SolvingEmployee.EmailAddress}"); }
            if (ticket.SolvingEmployee.Name != updatedTicket.SolvingEmployee.Name) { hasTicketUpdatedCorrectly = false; Console.WriteLine($"Name: {ticket.SolvingEmployee.Name} != {updatedTicket.SolvingEmployee.Name}"); }
            Console.BackgroundColor = ConsoleColor.Black;
            return hasTicketUpdatedCorrectly;
        }
        [HttpGet]
        [Authorize(Roles = "Service_Desk_Employee")]
        public async Task<IActionResult> UpdateSolvingEmployee(Ticket ticket)
        {
            try
            {
                return View(ticket);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "123";
                return RedirectToAction("Index", "Tickets");
            }
        }
        [HttpPost]
        [Authorize(Roles = "Service_Desk_Employee")]
        public async Task<IActionResult> UpdateSolvingEmployee(string ticketId, int solvingEmployeeNumber)
        {
            try
            {
                EmbeddedEmployee embeddedSolvingEmployee = await _employeeService.GetActiveEmbeddedSdEmployeeByIdAsync(solvingEmployeeNumber);
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
                TempData["ErrorMessage"] = "The update embeddedEmployee failed.";
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
