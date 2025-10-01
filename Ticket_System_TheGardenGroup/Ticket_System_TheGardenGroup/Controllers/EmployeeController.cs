using Microsoft.AspNetCore.Mvc;

namespace Ticket_System_TheGardenGroup.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
