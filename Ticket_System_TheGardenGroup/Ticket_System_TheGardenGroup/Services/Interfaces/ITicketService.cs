using MongoDB.Bson;
using Ticket_System_TheGardenGroup.Models;
using Ticket_System_TheGardenGroup.ViewModels;

namespace Ticket_System_TheGardenGroup.Services.Interfaces
{
    public interface ITicketService
    {
        Task<List<Ticket>> GetAllTickets();
        //Task<TicketViewModel> GetTicketAsync(ObjectId id);

        Task<Ticket> GetTicketByObjIdAsync(ObjectId objId);

        void UpdateTicket(Ticket ticket);

        void DeleteTicket(Ticket ticket);

        public Task ArchiveAllOldTicektsAsync(Ticket ticket);


    }
}
