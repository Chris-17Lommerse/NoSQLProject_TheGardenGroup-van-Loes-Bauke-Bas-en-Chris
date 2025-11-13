using MongoDB.Bson;
using Ticket_System_TheGardenGroup_With_Identity_Framework.Models;
using Ticket_System_TheGardenGroup_With_Identity_Framework.Repositories.Interfaces;
using Ticket_System_TheGardenGroup_With_Identity_Framework.Services.Interfaces;

namespace Ticket_System_TheGardenGroup_With_Identity_Framework.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<bool> CheckUpdatedTicket(Ticket ticket)
        {
            Ticket updatedTicket = await _ticketRepository.GetTicketByObjIdAsync(ticket.TicketId);
            if (updatedTicket == null || !Debugger(ticket, updatedTicket))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<List<Ticket>> FilterTicketsOnSearchInputAsync(string searchString)
        {
            if(searchString == null)
            {
                throw new ArgumentNullException(nameof(searchString));
            }

            if (searchString.Contains("AND") || searchString.Contains("OR"))
            {
                return await _ticketRepository.FilterTicketsOnAndOrSearchInputAsync(searchString);
            }
            else
            {
                return await _ticketRepository.FilterTicketsOnNormalSearchInputAsync(searchString);
            }
        }

        public Task<List<Ticket>> GetAllTickets()
        {
            return _ticketRepository.GetAllTickets();
        }
        /*public async Task<TicketViewModel> GetTicketAsync(ObjectId id)
        {
            Ticket ticket = await _ticketRepository.GetTicketAsync(id);
            TicketViewModel ticketViewModel = new TicketViewModel(ticket);
            return ticketViewModel;
        }*/

        public async Task<Ticket> GetTicketByObjIdAsync(ObjectId objId)
        {
            return await _ticketRepository.GetTicketByObjIdAsync(objId);
        }

        public void UpdateTicket(Ticket ticket)
        {
            _ticketRepository.UpdateTicket(ticket);
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
                Console.WriteLine($"DateTime.Compare: {DateTime.Compare(ticket.CreationTime, updatedTicket.CreationTime)}");
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
    }
}
