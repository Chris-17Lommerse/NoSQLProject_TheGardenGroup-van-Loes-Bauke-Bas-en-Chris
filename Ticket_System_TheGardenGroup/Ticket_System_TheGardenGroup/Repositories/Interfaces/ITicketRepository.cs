using Ticket_System_TheGardenGroup.Models;

namespace Ticket_System_TheGardenGroup.Repositories.Interfaces
{
    public interface ITicketRepository
    {
       Task<List<Ticket>> GetAllTickets();
    }
}
