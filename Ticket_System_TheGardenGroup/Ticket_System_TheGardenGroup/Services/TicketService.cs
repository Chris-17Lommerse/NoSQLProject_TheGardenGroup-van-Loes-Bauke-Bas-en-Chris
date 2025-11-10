using MongoDB.Bson;
using System.Security.Cryptography;
using Ticket_System_TheGardenGroup.Models;
using Ticket_System_TheGardenGroup.Repositories.Interfaces;
using Ticket_System_TheGardenGroup.Services.Interfaces;

namespace Ticket_System_TheGardenGroup.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public void ArchiveTicekt(Ticket ticket)
        {
            _ticketRepository.ArchiveTicekt(ticket);
        }

        public void DeleteTicket(Ticket ticket)
        {
            throw new NotImplementedException();
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

        //Keep track of how many times the button is pressed. The first time it will update the solving employee, the seccond time it will send it. 

    }
}
