using MongoDB.Driver;
using Ticket_System_TheGardenGroup.Models;
using Ticket_System_TheGardenGroup.Repositories.Interfaces;

namespace Ticket_System_TheGardenGroup.Repositories
{
    public class DBEmployeeRepository : IEmployeeRepository
    {
        private readonly IMongoCollection<Employee> _employeeCollection;

        public DBEmployeeRepository(IMongoDatabase database)
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

        public async Task<List<Employee>> GetAllEmployees()
        {
            /*
            List<Employee> employeeList = new List<Employee>();

            foreach (Employee employee in employeeList)
            {
                employeeList.Add(employee);
            }
            return employeeList;
            */

            return await _employeeCollection.Find(Builders<Employee>.Filter.Empty).ToListAsync(); 
            // Query 
            //db["EMPLOYEE"].find({ }, 
            //        {
            //_id: 1, employee_number: 1, password: 1, 
            //        employee_role: 1, name: 1, surname: 1, emailaddress: 1}
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
