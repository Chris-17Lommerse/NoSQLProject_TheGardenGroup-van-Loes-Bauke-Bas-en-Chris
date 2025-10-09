using Ticket_System_TheGardenGroup.Models;

namespace Ticket_System_TheGardenGroup.Repositories.Interfaces
{
    public interface ITicketRepository
    {
        Task<List<Ticket>> GetAllTickets(); //Waarom Task<>

        //voor het deleten waarschijnlijk een knop of zelfs automatisch

    }
}
