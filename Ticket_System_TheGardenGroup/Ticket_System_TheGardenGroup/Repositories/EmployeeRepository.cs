using MongoDB.Bson;
using MongoDB.Driver;
using System.Xml.Linq;
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

        public void AddEmployee(Employee employee)
        {
            _employeeCollection.InsertOneAsync(employee);
        }

        public async Task<List<EmployeeTicketsVm>> GetAllEmployeesWithAmountOfTicketsAsync()
        {
            var pipeline = new[]
            {
                // 1) lookup tickets
                new BsonDocument("$lookup", new BsonDocument
                {
                    {"from", "TICKETS" },
                    {"localField", "_id" },
                    {"foreignField", "employee_id"  },
                    {"as", "employee_tickets" }
                }),

                // 2) project the employee overview
                new BsonDocument("$project", new BsonDocument
                {
                    {"name", 1 },
                    {"surname", 1 },
                    {"emailaddress", 1 },
                    {"employee_number", 1 },
                    {"employee_role", 1 },
                    {"ticket_count",
                    // 3) fill the ticket_count array
                    new BsonDocument("$size", "$employee_tickets")}
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

        public void RemoveRegularEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public void RemoveServiceDeskEmployee(Employee employee)
        {
            throw new NotImplementedException();
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
                Builders<Employee>.Update.Set("isActive", employee.isActive)
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
                Builders<Employee>.Update.Set("isActive", employee.isActive)
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
                Builders<Employee>.Update.Set("isActive", employee.isActive)
            );
            await _employeeCollection.UpdateOneAsync(filter, combinedUpdate);
        }

        /*public async Task<EmployeeViewModel> GetEmployeeAsync(Employee employee)
        {
            var pipeline = new[]
            {
                // 1) find the user
                new BsonDocument("$match", new BsonDocument
                {
                    {"_id", employee.Id },
                }),

                // 2) project the user
                new BsonDocument("$project", new BsonDocument
                {
                    {"name", 1 },
                    {"surname", 1 },
                    {"emailaddress", 1 },
                    {"employee_number", 1 },
                    {"employee_role", 1 },
                    {"ticket_count",
                    new BsonDocument("$size", "$employee_tickets")}
                })
            };

            return await _employeeCollection
                         .Aggregate<EmployeeViewModel>(pipeline)();
        }*/

        void IEmployeeRepository.UpdateRegularEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        void IEmployeeRepository.UpdateServiceDeskEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
