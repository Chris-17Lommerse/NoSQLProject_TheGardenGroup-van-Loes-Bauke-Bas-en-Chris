using MongoDB.Driver;
using Ticket_System_TheGardenGroup_With_Identity_Framework.Models;
using Ticket_System_TheGardenGroup_With_Identity_Framework.Models.Enums;

namespace Ticket_System_TheGardenGroup.Repositories.ArchivingFuntionaliteit
{
    public class ArchivingTicketDbQueries
    {
        private readonly IMongoCollection<Ticket> _ticketCollection;

        public ArchivingTicketDbQueries(IMongoDatabase database)
        {
            _ticketCollection = database.GetCollection<Ticket>("TICKET");
        }

        public async Task<long> ArchiveAllOldTicektsAsyncClass()
        {
            try
            {
                var builder = Builders<Ticket>.Filter;
                var filter = builder.And(
                        builder.Lt(t => t.CreationTime, DateTime.UtcNow.AddYears(-2)),
                        builder.Ne(t => t.TicketStatus, TicketStatus.Closed)
                );

                var update = Builders<Ticket>.Update.Set(t => t.TicketStatus, TicketStatus.Closed);

                var result = await _ticketCollection.UpdateManyAsync(filter, update);

                long amountOfChangedDocuments = result.ModifiedCount;
                return amountOfChangedDocuments;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return 0;
            }
        }
        public async Task<List<Ticket>> GetAllUnArchivedTicketsAsyncClass()
        {
            return await _ticketCollection.Find(Builders<Ticket>.Filter.Ne(t => t.TicketStatus, TicketStatus.Closed)).ToListAsync();
        }
        public async Task<List<Ticket>> FindAllArchivedTicketsAsyncClass()
        {
            var creationTimeFilter = Builders<Ticket>.Filter.Lt(t => t.CreationTime, DateTime.UtcNow.AddYears(-2));
            var ticketStatusFilter = Builders<Ticket>.Filter.Eq(t => t.TicketStatus, TicketStatus.Closed);

            return await _ticketCollection.Find(Builders<Ticket>.Filter.And(creationTimeFilter, ticketStatusFilter)).ToListAsync();
        }
    }
}