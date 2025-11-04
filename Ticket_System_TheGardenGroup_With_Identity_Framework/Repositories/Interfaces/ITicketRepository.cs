using MongoDB.Bson;
using Ticket_System_TheGardenGroup_With_Identity_Framework.Models;

namespace Ticket_System_TheGardenGroup_With_Identity_Framework.Repositories.Interfaces
{
    public interface ITicketRepository
    {
        Task<List<Ticket>> GetAllTickets();
        void AddTicket(Ticket ticket);
        Task<Ticket> GetTicketAsync(ObjectId id);
        Task<Ticket> GetTicketByObjIdAsync(ObjectId objId);
        void UpdateTicket(Ticket ticket);
        Task<Ticket> FilterTicketsOnSearchInputAsync(string searchString);
    }
}
