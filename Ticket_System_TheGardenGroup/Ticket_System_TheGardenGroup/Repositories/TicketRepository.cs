using MongoDB.Driver;
using Ticket_System_TheGardenGroup.Models;
using Ticket_System_TheGardenGroup.Repositories.Interfaces;

namespace Ticket_System_TheGardenGroup.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly IMongoCollection<Ticket> _ticketCollection;

        public TicketRepository(IMongoDatabase database)
        {
            _ticketCollection = database.GetCollection<Ticket>("TICKET");
        }

    }
}
