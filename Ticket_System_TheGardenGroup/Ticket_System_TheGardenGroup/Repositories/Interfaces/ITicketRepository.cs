using MongoDB.Bson;
using Ticket_System_TheGardenGroup.Models;

namespace Ticket_System_TheGardenGroup.Repositories.Interfaces
{
    public interface ITicketRepository
    {
        Task<List<Ticket>> GetAllTickets();
        Task<Ticket> GetTicketAsync(ObjectId id);
		Task<Ticket> GetTicketByObjIdAsync(ObjectId objId);

        void AddTicket(Ticket ticket);
        void UpdateTicket(Ticket ticket);
        void DeleteTicket(Ticket ticket);

        public Task<long> ArchiveAllOldTicektsAsync();
        public Task<List<Ticket>> GetAllUnArchivedTicketsAsync();
        public Task<List<Ticket>> FindAllArchivedTicketsAsync();
    }
}
