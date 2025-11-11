using MongoDB.Bson;
using Ticket_System_TheGardenGroup.Models;

namespace Ticket_System_TheGardenGroup.Repositories.Interfaces
{
    public interface ITicketRepository
    {
        Task<List<Ticket>> GetAllTickets();
        void AddTicket(Ticket ticket);
        Task<Ticket> GetTicketAsync(ObjectId id);
		Task<Ticket> GetTicketByObjIdAsync(ObjectId objId);
        void UpdateTicket(Ticket ticket);
        void DeleteTicket(Ticket ticket);
        public Task ArchiveAllOldTicektsAsync(Ticket ticket);
    }
}
