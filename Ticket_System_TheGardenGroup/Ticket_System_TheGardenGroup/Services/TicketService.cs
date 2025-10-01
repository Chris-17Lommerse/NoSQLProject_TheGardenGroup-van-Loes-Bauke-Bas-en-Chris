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
    }
}
