using MongoDB.Bson;
using Ticket_System_TheGardenGroup.Models;
using Ticket_System_TheGardenGroup.Repositories;
using Ticket_System_TheGardenGroup.Repositories.Interfaces;
using Ticket_System_TheGardenGroup.Services.Interfaces;
using Ticket_System_TheGardenGroup.ViewModels;

namespace Ticket_System_TheGardenGroup.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public Task<List<Ticket>> GetAllTickets()
        {
            return _ticketRepository.GetAllTickets();
        }
        public async Task<TicketViewModel> GetTicketAsync(ObjectId id)
        {
            Ticket ticket = await _ticketRepository.GetTicketAsync(id);
            TicketViewModel ticketViewModel = new TicketViewModel(ticket);
            return ticketViewModel;
        }
    }
}
