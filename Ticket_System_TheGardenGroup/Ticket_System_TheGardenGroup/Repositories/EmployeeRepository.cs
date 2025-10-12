using MongoDB.Bson;
using MongoDB.Driver;
using Ticket_System_TheGardenGroup.Models;
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
            var filter = Builders<Employee>.Filter.Eq("employee_number", employeeNumber);
            return await _employeeCollection.Find(filter).FirstOrDefaultAsync();
        }

        public void AddEmployee(Employee employee)
        {
            _employeeCollection.InsertOneAsync(employee);
        }

        public async Task<List<EmployeeTicketsVm>> GetAllEmployeesWithAmountOfTicketsAsync()
        {
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
                    { "_id", 0 },
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

        public async Task UpdateEmployee(Employee employee)
        {
                var filter = Builders<Employee>.Filter.Eq("_id", employee.Id);
                var combinedUpdate = Builders<Employee>.Update.Combine(
                    Builders<Employee>.Update.Set("name", employee.Name),
                    Builders<Employee>.Update.Set("surname", employee.Surname),
                    Builders<Employee>.Update.Set("emailaddress", employee.EmailAddress),
                    Builders<Employee>.Update.Set("employee_role", employee.EmployeeRole),
                    Builders<Employee>.Update.Set("password", employee.Password),
                    Builders<Employee>.Update.Set("isActive", employee.IsActive)
                );
                await _employeeCollection.UpdateOneAsync(filter, combinedUpdate);
        }
        public async Task UpdateRegularEmployee(Employee employee)
        {
            var filter = Builders<Employee>.Filter.Eq("_id", employee.Id);
            var combinedUpdate = Builders<Employee>.Update.Combine(
                Builders<Employee>.Update.Set("name", employee.Name),
                Builders<Employee>.Update.Set("surname", employee.Surname),
                Builders<Employee>.Update.Set("emailaddress", employee.EmailAddress),
                Builders<Employee>.Update.Set("password", employee.Password),
                Builders<Employee>.Update.Set("isActive", employee.IsActive)
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
                Builders<Employee>.Update.Set("isActive", employee.IsActive)
            );
            await _employeeCollection.UpdateOneAsync(filter, combinedUpdate);
        }
    }
}
