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

        public void AddEmployee(Employee employee)
        {
            _employeeCollection.InsertOneAsync(employee);
        }

        public async Task<List<EmployeeTicketsViewModel>> GetAllEmployeesWithAmountOfTicketsAsync()
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

                // 2) project the user overview
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
                         .Aggregate<EmployeeTicketsViewModel>(pipeline)
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

        public void UpdateRegularEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public void UpdateServiceDeskEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
