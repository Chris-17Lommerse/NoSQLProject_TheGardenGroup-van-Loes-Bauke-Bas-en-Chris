using MongoDB.Bson;
using MongoDB.Driver;
using Ticket_System_TheGardenGroup_With_Identity_Framework.Models;
using Ticket_System_TheGardenGroup_With_Identity_Framework.Repositories.Interfaces;
using Ticket_System_TheGardenGroup_With_Identity_Framework.ViewModels;

namespace Ticket_System_TheGardenGroup_With_Identity_Framework.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IMongoCollection<Employee> _employeeCollection;

        public EmployeeRepository(IMongoDatabase database)
        {
            _employeeCollection = database.GetCollection<Employee>("EMPLOYEE");
        }

        public async Task<Employee> GetEmployeeByEmployeeIdAsync(ObjectId employeeID)
        {
            var filter = Builders<Employee>.Filter.Eq(e => e.Id, employeeID);
            return await _employeeCollection.Find(filter).FirstOrDefaultAsync();
        }
        public async Task<Employee> GetEmployeeByEmployeeNumberAsync(int employeeNumber)
        {
            var filter = Builders<Employee>.Filter.Eq(e => e.EmployeeNumber, employeeNumber);
            return await _employeeCollection.Find(filter).FirstOrDefaultAsync();
        }

        public void AddEmployeeAsync(Employee employee)
        {
            _employeeCollection.InsertOneAsync(employee);
        }

        public async Task<List<EmployeeTicketsVm>> GetAllEmployeesWithAmountOfTicketsAsync()
        {
            //deze pipeline moet even worden gecheckt op bugs met het nieuwe veld. 
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

        public async Task RemoveEmployeeAsync(Employee employee)
        {
            var filter = Builders<Employee>.Filter.Eq("_id", employee.Id);
            var combinedUpdate = Builders<Employee>.Update.Combine(
                Builders<Employee>.Update.Set("isActive", !employee.IsActive)
            );
            await _employeeCollection.UpdateOneAsync(filter, combinedUpdate);
        }
        public async Task RemoveRegularEmployeeAsync(Employee employee)
        {
            var filter = Builders<Employee>.Filter.Eq("_id", employee.Id);
            var combinedUpdate = Builders<Employee>.Update.Combine(
                Builders<Employee>.Update.Set("isActive", !employee.IsActive)
            );
            await _employeeCollection.UpdateOneAsync(filter, combinedUpdate);
        }

        public async Task RemoveServiceDeskEmployeeAsync(Employee employee)
        {
            var filter = Builders<Employee>.Filter.Eq("_id", employee.Id);
            var combinedUpdate = Builders<Employee>.Update.Combine(
                Builders<Employee>.Update.Set("isActive", employee.IsActive)
            );
            await _employeeCollection.UpdateOneAsync(filter, combinedUpdate);
        }
        public async Task UpdateEmployeeAsync(Employee employee)
        {
            var filter = Builders<Employee>.Filter.Eq("_id", employee.Id);
            var combinedUpdate = Builders<Employee>.Update.Combine(
                Builders<Employee>.Update.Set("name", employee.Name),
                Builders<Employee>.Update.Set("surname", employee.Surname),
                Builders<Employee>.Update.Set("emailaddress", employee.EmailAddress),
                Builders<Employee>.Update.Set("employee_role", employee.EmployeeRole),
                Builders<Employee>.Update.Set("password", employee.Password),
                Builders<Employee>.Update.Set("isActive", employee.IsActive),
                Builders<Employee>.Update.Set("workingOn", employee.WorkingOn)
            );
            await _employeeCollection.UpdateOneAsync(filter, combinedUpdate);
        }
        public async Task UpdateEmployeeWorkingOnAsync(Employee employee)
        {
            var filter = Builders<Employee>.Filter.Eq("_id", employee.Id);
            var combinedUpdate = Builders<Employee>.Update.Combine(Builders<Employee>.Update.Set("workingOn", employee.WorkingOn)
            );
            await _employeeCollection.UpdateOneAsync(filter, combinedUpdate);
        }

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

            return await _employeeCollection.Aggregate<EmbeddedEmployee>(pipeline).FirstOrDefaultAsync(); //Must return one document, else error ~ Bas
        }
        public void UpdateEmployeeViewModelAsync(EmployeeViewModel employeeViewModel)
        {
            var filter = Builders<Employee>.Filter.Eq("_id", employeeViewModel.Id);
            var combinedUpdate = Builders<Employee>.Update.Combine(

                Builders<Employee>.Update.Set("employee_number", employeeViewModel.EmployeeNumber),
                Builders<Employee>.Update.Set("name", employeeViewModel.Name),
                Builders<Employee>.Update.Set("surname", employeeViewModel.Surname),
                Builders<Employee>.Update.Set("emailaddress", employeeViewModel.EmailAddress),
                Builders<Employee>.Update.Set("employee_role", employeeViewModel.EmployeeRole.ToString()),
                Builders<Employee>.Update.Set("isActive", employeeViewModel.IsActive)
            );
            Console.WriteLine(filter);
            Console.WriteLine(combinedUpdate);
            _employeeCollection.UpdateOneAsync(filter, combinedUpdate);
        }

        void IEmployeeRepository.UpdateEmployeeAsync(Employee employee)
        {
            var filter = Builders<Employee>.Filter.Eq("_id", employee.Id);
            var combinedUpdate = Builders<Employee>.Update.Combine(

                Builders<Employee>.Update.Set("employee_number", employee.EmployeeNumber),
                Builders<Employee>.Update.Set("password", employee.Password),
                Builders<Employee>.Update.Set("name", employee.Name),
                Builders<Employee>.Update.Set("surname", employee.Surname),
                Builders<Employee>.Update.Set("employee_role", employee.EmployeeRole.ToString()),
                Builders<Employee>.Update.Set("isActive", employee.IsActive),
                Builders<Employee>.Update.Set("isActive", employee.WorkingOn),
                Builders<Employee>.Update.Set("working_on", employee.EmailAddress)
            );
            Console.WriteLine(filter);
            Console.WriteLine(combinedUpdate);
            
            _employeeCollection.UpdateOneAsync(filter, combinedUpdate);
        }
    }
}
