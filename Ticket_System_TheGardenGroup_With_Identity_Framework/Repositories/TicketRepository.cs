using MongoDB.Bson;
using MongoDB.Driver;
using System;
using Ticket_System_TheGardenGroup_With_Identity_Framework.Models;
using Ticket_System_TheGardenGroup_With_Identity_Framework.Repositories.Interfaces;

namespace Ticket_System_TheGardenGroup_With_Identity_Framework.Repositories
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

        public async Task<List<Ticket>> FilterTicketsOnAndOrSearchInputAsync(string searchString)
        {
            var builder = Builders<Ticket>.Filter;
            var sortBuilder = Builders<Ticket>.Sort;

            string[] orGroups = searchString.Split(new[] { " OR ", " or " }, StringSplitOptions.RemoveEmptyEntries);

            List<FilterDefinition<Ticket>> orFilters = new List<FilterDefinition<Ticket>>();

            foreach (string orGroup in orGroups)
            {
                string[] andParts = orGroup.Split(new[] { " AND ", " and " }, StringSplitOptions.RemoveEmptyEntries);

                List<FilterDefinition<Ticket>> andFilters = new List<FilterDefinition<Ticket>>();

                foreach (string part in andParts)
                {
                    string[] pieces = part.Split(":", 2);
                    if (pieces.Length == 2)
                    {
                        string field = pieces[0].Trim();
                        string value = pieces[1].Trim();

                        andFilters.Add(builder.Regex(field, new BsonRegularExpression(value, "i")));
                    }
                }

                if (andFilters.Count > 0)
                {
                    orFilters.Add(builder.And(andFilters));
                }
            }

            FilterDefinition<Ticket> finalFilter = orFilters.Count > 0 ? builder.Or(orFilters) : FilterDefinition<Ticket>.Empty;

            var sortByCreationDate = sortBuilder.Descending("creation_time");

            return await _ticketCollection.Find(finalFilter).Sort(sortByCreationDate).ToListAsync();

        }

        public async Task<List<Ticket>> FilterTicketsOnNormalSearchInputAsync(string searchString)
        {
            var builders = Builders<Ticket>.Filter;
            var sortBuilder = Builders<Ticket>.Sort;

            List<FilterDefinition<Ticket>> finalFilter = new List<FilterDefinition<Ticket>>();

            BsonRegularExpression regex = new BsonRegularExpression(searchString, "i");

            finalFilter.Add(builders.Regex("ticket_name", regex));
            finalFilter.Add(builders.Regex("ticcket_status", regex));
            finalFilter.Add(builders.Regex("ticket_escalation_description", regex));
            finalFilter.Add(builders.Regex("reporting_employee", regex));
            finalFilter.Add(builders.Regex("solving_employee", regex));
            finalFilter.Add(builders.Regex("priority", regex));
            

            FilterDefinition<Ticket> combinedFilter;

            if (finalFilter.Count > 0)
            {
                combinedFilter = builders.Or(finalFilter);
            }
            else
            {
                combinedFilter = FilterDefinition<Ticket>.Empty;
            }

            var sortByCreationDate = sortBuilder.Descending("creation_time");

            return await _ticketCollection.Find(combinedFilter).Sort(sortByCreationDate).ToListAsync();
        }

        public async Task<List<Ticket>> GetAllTicketsByEmailAddress(string emailAddress)
        {
            var filter = Builders<Ticket>.Filter.Eq("reporting_employee.emailaddress", emailAddress);
            return await _ticketCollection.Find(Builders<Ticket>.Filter.Empty).ToListAsync();
        }
        public async Task<Ticket> GetTicketAsync(ObjectId id)
        {
            var filter = Builders<Ticket>.Filter.Eq("_id", id);
            return await _ticketCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<Ticket> GetTicketByObjIdAsync(ObjectId ticketId)
        {
            //var filter = Builders<Ticket>.Filter.Eq("_id", objId);
            //var filter = Builders<Ticket>.Filter.Eq(t => t.TicketId, objId);

            /*var filter = Builders<Ticket>.Filter.Eq("TicketId", objId);
			Ticket ticket = await _ticketCollection.Find(filter).FirstOrDefaultAsync();*/

            var filter = Builders<Ticket>.Filter.Eq(t => t.TicketId, ticketId);
            return await _ticketCollection.Find(filter).FirstOrDefaultAsync();

            //return ticket;

        }

        //TODO reporting_employee and solving_employee can not be updated yet
        public void UpdateTicket(Ticket ticket)
        {
            var filter = Builders<Ticket>.Filter.Eq("_id", ticket.TicketId);
            var combinedUpdate = Builders<Ticket>.Update.Combine(
                Builders<Ticket>.Update.Set("ticket_name", ticket.TicketName),
                Builders<Ticket>.Update.Set("ticket_status", ticket.TicketStatus),
                Builders<Ticket>.Update.Set("description", ticket.Description),
                Builders<Ticket>.Update.Set("ticket_escalation_description", ticket.TicketEscalationDescription),
                Builders<Ticket>.Update.Set("is_solved", ticket.IsSolved),
                Builders<Ticket>.Update.Set("priority", ticket.Priority)
            );
            _ticketCollection.UpdateOneAsync(filter, combinedUpdate);
        }
    }
}
