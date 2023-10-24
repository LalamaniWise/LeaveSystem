using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace leaveSystem.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController1 : Controller
    {
        public IActionResult Display()
        {
            return View();
        }
    }
}
