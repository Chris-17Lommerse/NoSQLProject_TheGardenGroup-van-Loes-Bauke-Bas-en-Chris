using MongoDB.Bson;
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

        public async Task<List<Ticket>> GetAllTickets()
        {   
            return await _ticketCollection.Find(Builders<Ticket>.Filter.Empty).ToListAsync();
        }
    }
}
