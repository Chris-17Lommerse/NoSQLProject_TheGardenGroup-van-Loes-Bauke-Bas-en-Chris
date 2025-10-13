using MongoDB.Bson;
using Ticket_System_TheGardenGroup.Models;

namespace Ticket_System_TheGardenGroup.Repositories.Interfaces
{
    public interface ITicketRepository
    {
        Task<List<Ticket>> GetAllTickets();
        void AddTicket(Ticket ticket);
        Task UpdateTicket(Ticket ticket);
        Task<Ticket> GetTicketAsync(ObjectId id);

		Task<Ticket> GetTicketByObjIdAsync(ObjectId objId);
	}
}
