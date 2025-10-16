using Microsoft.AspNetCore.Mvc;

namespace Ticket_System_TheGardenGroup_With_Identity_Framework.Controllers
{
    public class EmployeesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
