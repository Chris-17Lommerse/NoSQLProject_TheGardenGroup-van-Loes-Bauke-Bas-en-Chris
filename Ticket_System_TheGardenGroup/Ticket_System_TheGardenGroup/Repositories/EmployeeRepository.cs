using MongoDB.Bson;
using MongoDB.Driver;
using Ticket_System_TheGardenGroup.Models;
using Ticket_System_TheGardenGroup.Models.Enums;
using Ticket_System_TheGardenGroup.Repositories.Interfaces;
using Ticket_System_TheGardenGroup.ViewModels;

namespace Ticket_System_TheGardenGroup.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IMongoCollection<Employee> _employeeCollection;

        public EmployeeRepository(IMongoDatabase database)
        {
            _employeeCollection = database.GetCollection<Employee>("EMPLOYEE");
        }

        public async Task<Employee> GetEmployeeAsync(int employeeNumber)
        {
			/*var filter = Builders<Employee>.Filter.Eq("employee_number", employeeNumber);
            return await _employeeCollection.Find(filter).FirstOrDefaultAsync();*/

			var filter = Builders<Employee>.Filter.Eq(e => e.EmployeeNumber, employeeNumber);
			return await _employeeCollection.Find(filter).FirstOrDefaultAsync();
		}

        public void AddEmployee(Employee employee)
        {
            _employeeCollection.InsertOneAsync(employee);
        }

        public async Task<List<EmployeeTicketsVm>> GetAllEmployeesWithAmountOfTicketsAsync()
        {
            //deze pipeline moet even worden gecheck op bugs met het nieuwe veld. 
            var pipeline = new List<BsonDocument>
            {
                new BsonDocument("$group", new BsonDocument
                {
                    { "_id", "$employee_number" },
                    { "TotalTickets", new BsonDocument("$sum", 1) },
                    { "employeeDetails", new BsonDocument("$first", "$$ROOT") }
                }),

                new BsonDocument("$project", new BsonDocument
                {
					{ "_id", "$employeeDetails._id" },
					//{ "_id", 0 },
                    { "employee_number", "$_id" },
                    { "TotalTickets", 1 },
                    { "surname", "$employeeDetails.surname" },
                    { "name", "$employeeDetails.name" },
                    { "emailaddress", "$employeeDetails.emailaddress" },
                    { "employee_role", "$employeeDetails.employee_role" }
                })
            };

                    return await _employeeCollection
                        .Aggregate<EmployeeTicketsVm>(pipeline)
                        .ToListAsync();
                }

        public List<Employee> GetAllRegularEmployees()
        {
            List<Employee> employees = new List<Employee>();
            // Query
            //db["EMPLOYEE"].find({ employee_role: "REGULAR_EMPLOYEE"}, 
            //        {
            //_id: 1, employee_number: 1, password: 1, 
            //        employee_role: 1, name: 1, surname: 1, emailaddress: 1})
            return employees;
        }

        public List<Employee> GetAllServiceDeskEmployees()
        {
            List<Employee> employees = new List<Employee>();
            //db["EMPLOYEE"].find({ employee_role: "SERVICE_DESK_EMPLOYEE"}, 
            //        {
            //_id: 1, employee_number: 1, password: 1, 
            //        employee_role: 1, name: 1, surname: 1, emailaddress: 1})
            return employees;
        }

        public async Task RemoveEmployee(Employee employee)
        {
            var filter = Builders<Employee>.Filter.Eq("_id", employee.Id);
            var combinedUpdate = Builders<Employee>.Update.Combine(
                Builders<Employee>.Update.Set("isActive", !employee.IsActive)
            );
            await _employeeCollection.UpdateOneAsync(filter, combinedUpdate);
        }
        public async Task RemoveRegularEmployee(Employee employee)
        {
            var filter = Builders<Employee>.Filter.Eq("_id", employee.Id);
            var combinedUpdate = Builders<Employee>.Update.Combine(
                Builders<Employee>.Update.Set("isActive",  !employee.IsActive)
            );
            await _employeeCollection.UpdateOneAsync(filter, combinedUpdate);
        }

        public async Task RemoveServiceDeskEmployee(Employee employee)
        {
            var filter = Builders<Employee>.Filter.Eq("_id", employee.Id);
            var combinedUpdate = Builders<Employee>.Update.Combine(
                Builders<Employee>.Update.Set("isActive", employee.IsActive)
            );
            await _employeeCollection.UpdateOneAsync(filter, combinedUpdate);
        }
        //Ik zou gewoon deze gebruiken. 
        public async Task UpdateEmployee(Employee employee)
        {
                var filter = Builders<Employee>.Filter.Eq("_id", employee.Id);
                var combinedUpdate = Builders<Employee>.Update.Combine(
                    Builders<Employee>.Update.Set("name", employee.Name),
                    Builders<Employee>.Update.Set("surname", employee.Surname),
                    Builders<Employee>.Update.Set("emailaddress", employee.EmailAddress),
                    Builders<Employee>.Update.Set("employee_role", employee.EmployeeRole),
                    Builders<Employee>.Update.Set("password", employee.Password),
                    Builders<Employee>.Update.Set("isActive", employee.IsActive)//,
                    //Builders<Employee>.Update.Set("workingOn", employee.WorkingOn)
                );
                await _employeeCollection.UpdateOneAsync(filter, combinedUpdate);
        }

        //Waarom Deze twee, Dit is super redundant
        public async Task UpdateRegularEmployee(Employee employee)
        {
            var filter = Builders<Employee>.Filter.Eq("_id", employee.Id);
            var combinedUpdate = Builders<Employee>.Update.Combine(
                Builders<Employee>.Update.Set("name", employee.Name),
                Builders<Employee>.Update.Set("surname", employee.Surname),
                Builders<Employee>.Update.Set("emailaddress", employee.EmailAddress),
                Builders<Employee>.Update.Set("password", employee.Password),
                Builders<Employee>.Update.Set("isActive", employee.IsActive)//,
                //Builders<Employee>.Update.Set("workingOn", employee.WorkingOn)
            );
            await _employeeCollection.UpdateOneAsync(filter, combinedUpdate);
        }

        public async Task UpdateServiceDeskEmployee(Employee employee)
        {
            var filter = Builders<Employee>.Filter.Eq("_id", employee.Id);
            var combinedUpdate = Builders<Employee>.Update.Combine(
                Builders<Employee>.Update.Set("name", employee.Name),
                Builders<Employee>.Update.Set("surname", employee.Surname),
                Builders<Employee>.Update.Set("emailaddress", employee.EmailAddress),
                Builders<Employee>.Update.Set("password", employee.Password),
                Builders<Employee>.Update.Set("isActive", employee.IsActive)//,
                //Builders<Employee>.Update.Set("workingOn", employee.WorkingOn)
            );
            await _employeeCollection.UpdateOneAsync(filter, combinedUpdate);
        }
        // einde "deze twee"

        //Wil ik hier employee_role ook een variable maken? Eigenlijk wil ik hier maar 1 tje uitkrijgen, hoewel dat altijd het geval is. hmmm....
        public async Task<EmbeddedEmployee> GetActiveEmbeddedSdEmployeeByIdAsync(int employeeNumber)
        {
            var pipeline = new[]
            {
                new BsonDocument("$match", new BsonDocument
                {
                    { "employee_number", employeeNumber },
                    { "employee_role", "Service_Desk_Employee" },
                    { "isActive", true }
                }),

                new BsonDocument("$project", new BsonDocument
                {
                    { "_id", 0 },
                    { "surname", 0 },
                    { "password", 0 },
                    { "isActive", 0 },
                    { "priority", 0 }
                })
            };

            return await _employeeCollection.Aggregate<EmbeddedEmployee>(pipeline).FirstOrDefaultAsync(); 
        }

        public async Task FindTicketIdInWorkingOnArryAsync(ObjectId ticketId, List<ObjectId> connectedTicketIdsToRemove)
        {
            throw new NotImplementedException("This method is not yet implemented");

            /*// Stap 1: Vind het Ticket op basis van ticketId
            var filter = Builders<Employee>.Filter.Eq(t => t.WorkingOn., ticketId);
            var employee = await _employeeCollection.Find(filter).FirstOrDefaultAsync();

            if (employee != null)
            {
                // Stap 2: Lees de lijst van verbonden Tickets
                var currentWorkingOn = employee.WorkingOn ?? new List<EmbeddedEmployee>();

                // Stap 3: Verwijder de verbonden Tickets die in connectedTicketIdsToRemove staan
                var updatedWorkingOn = currentWorkingOn
                    .Where(t => !connectedTicketIdsToRemove.Contains(t.Id))
                    .ToList();

                // Stap 4: Update de lijst in het Ticket
                employee.WorkingOn = updatedWorkingOn;

                // Stap 5: Sla het bijgewerkte Ticket weer op in de database
                await _employeeCollection.ReplaceOneAsync(filter, employee);
            }*/
        }
    }
}
