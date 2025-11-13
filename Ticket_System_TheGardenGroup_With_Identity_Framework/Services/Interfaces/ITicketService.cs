using MongoDB.Bson;
using Ticket_System_TheGardenGroup_With_Identity_Framework.Models;

namespace Ticket_System_TheGardenGroup_With_Identity_Framework.Services.Interfaces
{
    public interface ITicketService
    {
        Task<List<Ticket>> GetAllTicketsByEmailAddress(string emailAddress);
        Task<List<Ticket>> GetTicketsByEmployeeId();
        //Task<TicketViewModel> GetTicketAsync(ObjectId id);

        Task<Ticket> GetTicketByObjIdAsync(ObjectId objId);

        void UpdateTicket(Ticket ticket);

        Task<List<Ticket>> FilterTicketsOnSearchInputAsync(string searchString);

    }
}
