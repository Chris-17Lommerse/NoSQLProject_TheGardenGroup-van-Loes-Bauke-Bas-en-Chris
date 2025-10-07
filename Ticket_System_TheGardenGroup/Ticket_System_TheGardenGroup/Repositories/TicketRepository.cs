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
            // Query
            //List<Ticket> tickets = await _ticketCollection.Find({ },
            //      {_id: 1 ticket_id: 1, creation_time: 1, ticket_status: 1, 
            //       description: 1, solving_employee: 1, reporting_employee: 1,
            //      is_solved: 1, ticket_escalation_description: 1} )
            return await _ticketCollection.Find(Builders<Ticket>.Filter.Empty).ToListAsync();
        }
    }
}
