using MongoDB.Driver;
using Ticket_System_TheGardenGroup.Models;
using Ticket_System_TheGardenGroup.Repositories.Interfaces;

namespace Ticket_System_TheGardenGroup.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IMongoCollection<Employee> _employeeCollection;

        public EmployeeRepository(IMongoDatabase database)
        {
            _employeeCollection = database.GetCollection<Employee>("EMPLOYEE");
        }

        public void AddRegularEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public void AddServiceDeskEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public List<Employee> GetAllRegularEmployees()
        {
            throw new NotImplementedException();
        }

        public List<Employee> GetAllServiceDeskEmployees()
        {
            throw new NotImplementedException();
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
