using MongoDB.Bson;
using Ticket_System_TheGardenGroup_With_Identity_Framework.Models;

namespace Ticket_System_TheGardenGroup_With_Identity_Framework.Services.Interfaces
{
    public interface ITicketService
    {
        Task<List<Ticket>> GetAllTickets();
        Task<Ticket> GetTicketByObjIdAsync(ObjectId objId);
        void UpdateTicket(Ticket ticket);
        Task <bool> CheckUpdatedTicket(Ticket ticket);
        Task<List<Ticket>> FilterTicketsOnSearchInputAsync(string searchString);
    }
}
