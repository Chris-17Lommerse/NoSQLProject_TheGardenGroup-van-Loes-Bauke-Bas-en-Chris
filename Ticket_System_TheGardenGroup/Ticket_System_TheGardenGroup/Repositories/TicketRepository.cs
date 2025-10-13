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

        public void AddTicket(Ticket ticket)
        {
            _ticketCollection.InsertOneAsync(ticket);
        }

        public async Task<List<Ticket>> GetAllTickets()
        {   
            return await _ticketCollection.Find(Builders<Ticket>.Filter.Empty).ToListAsync();
        }
        public async Task<Ticket> GetTicketAsync(ObjectId id)
        {
            var filter = Builders<Ticket>.Filter.Eq("_id", id);
            return await _ticketCollection.Find(filter).FirstOrDefaultAsync();
        }

		public async Task<Ticket> GetTicketByObjIdAsync(ObjectId objId)
		{
			var filter = Builders<Ticket>.Filter.Eq("_id", objId);

			Ticket ticket = await _ticketCollection.Find(filter).FirstOrDefaultAsync();
            
            return ticket;

		}

		//TODO reporting_employee and solving_employee can not be updated yet
		public async Task UpdateTicket(Ticket ticket)
        {
            var filter = Builders<Ticket>.Filter.Eq("_id", ticket.TicketId);
            var combinedUpdate = Builders<Ticket>.Update.Combine(
                Builders<Ticket>.Update.Set("ticket_name", ticket.TicketName),
                Builders<Ticket>.Update.Set("ticket_status", ticket.TicketStatus),
                Builders<Ticket>.Update.Set("description", ticket.Description),
                Builders<Ticket>.Update.Set("ticket_escalation_description", ticket.TicketEscalationDescription),
                Builders<Ticket>.Update.Set("is_solved", ticket.IsSolved)
            );
            await _ticketCollection.UpdateOneAsync(filter, combinedUpdate);
        }
    }
}
