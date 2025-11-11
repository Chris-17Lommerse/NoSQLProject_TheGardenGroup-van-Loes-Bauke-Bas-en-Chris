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
            if (string.IsNullOrWhiteSpace(searchString))
            {
                return await _ticketCollection.Find(FilterDefinition<Ticket>.Empty).ToListAsync();
            }

            var orGroups = searchString.Split(new[] { " OR ", " or " }, StringSplitOptions.RemoveEmptyEntries);

            var orFilters = new List<FilterDefinition<Ticket>>();
            var builder = Builders<Ticket>.Filter;

            foreach (var orGroup in orGroups)
            {
                var andParts = orGroup.Split(new[] { " AND ", " and " }, StringSplitOptions.RemoveEmptyEntries);

                var andFilters = new List<FilterDefinition<Ticket>>();

                foreach (var part in andParts)
                {
                    var pieces = part.Split(":", 2);
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

            var finalFilter = orFilters.Count > 0 ? builder.Or(orFilters) : FilterDefinition<Ticket>.Empty;

            return await _ticketCollection.Find(finalFilter).ToListAsync();

        }

        public async Task<List<Ticket>> FilterTicketsOnNormalSearchInputAsync(string searchString)
        {
            var builders = Builders<Ticket>.Filter;
            var finalFilter = new List<FilterDefinition<Ticket>>
            {
                builders.Eq("creation_time", searchString),
                builders.Eq("ticket_status", searchString),
                builders.Eq("ticket_name", searchString),
                builders.Eq("ticket_escalation_description", searchString),
                builders.Eq("is_solved", searchString),
                builders.Eq("reporting_employee", searchString),
                builders.Eq("solving_employee", searchString),
                builders.Eq("priority", searchString)
            };

            FilterDefinition<Ticket> combinedFilter;

            if (finalFilter.Count > 0)
            {
                combinedFilter = builders.Or(finalFilter);
            }
            else
            {
                combinedFilter = FilterDefinition<Ticket>.Empty;
            }

            return await _ticketCollection.Find(combinedFilter).ToListAsync();
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

        public async Task<Ticket> GetTicketByObjIdAsync(ObjectId ticketId)
        {
            var filter = Builders<Ticket>.Filter.Eq(t => t.TicketId, ticketId);
            return await _ticketCollection.Find(filter).FirstOrDefaultAsync();
        }

        public void UpdateTicket(Ticket ticket)
        {
            var filter = Builders<Ticket>.Filter.Eq("_id", ticket.TicketId);
            var combinedUpdate = Builders<Ticket>.Update.Combine(
            //Ticket
                Builders<Ticket>.Update.Set("ticket_name", ticket.TicketName),
                Builders<Ticket>.Update.Set("ticket_status", ticket.TicketStatus.ToString()),
                Builders<Ticket>.Update.Set("description", ticket.Description),
                Builders<Ticket>.Update.Set("ticket_escalation_description", ticket.TicketEscalationDescription),
                Builders<Ticket>.Update.Set("is_solved", ticket.IsSolved),
                Builders<Ticket>.Update.Set("priority", ticket.Priority.ToString()),
            //EmbeddedSolvingEmployee
                Builders<Ticket>.Update.Set("solving_employee.employee_number", ticket.SolvingEmployee.EmployeeNumber),
                Builders<Ticket>.Update.Set("solving_employee.employee_role", ticket.SolvingEmployee.EmployeeRole.ToString()),
                Builders<Ticket>.Update.Set("solving_employee.emailaddress", ticket.SolvingEmployee.EmailAddress),
                Builders<Ticket>.Update.Set("solving_employee.name", ticket.SolvingEmployee.Name)
            );
            Console.WriteLine(filter);
            Console.WriteLine(combinedUpdate);
            _ticketCollection.UpdateOneAsync(filter, combinedUpdate);
        }
    }
}
