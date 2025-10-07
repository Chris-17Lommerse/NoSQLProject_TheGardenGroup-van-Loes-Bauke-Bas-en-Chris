using Ticket_System_TheGardenGroup.Models;

namespace Ticket_System_TheGardenGroup.Services.Interfaces
{
    public interface ITicketService
    {
        Task<List<Ticket>> GetAllTickets();
    }
}
