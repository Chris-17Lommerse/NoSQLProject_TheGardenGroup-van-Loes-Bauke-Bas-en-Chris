using Microsoft.AspNetCore.Identity;
using MongoDB.Driver.Linq;
using Ticket_System_TheGardenGroup_With_Identity_Framework.Models.Enums;

namespace Ticket_System_TheGardenGroup_With_Identity_Framework.Models
{
    public static class RoleSeeder
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            foreach(EmployeeRole employeeRole in Enum.GetValues(typeof(EmployeeRole)))
            {
                string employeeRoleToString = employeeRole.ToString();
                if(!await roleManager.RoleExistsAsync(employeeRoleToString))
                {
                    await roleManager.CreateAsync(new IdentityRole(employeeRoleToString));
                }
            }
        }
    }
}
