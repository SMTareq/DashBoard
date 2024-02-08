using GAMEPORTALCMS.Repository.Implementation;
using Microsoft.AspNetCore.Mvc;

namespace GAMEPORTALCMS.Controllers
{
    public class DashboardController : BaseController
    {
        public IActionResult Index()
        {
            ViewBag.UserName = UserName;
            return View();
        }

      
        public IActionResult ClearSession()
        {
            HttpContext.Session.Clear();
            return Ok("Session cleared.");
        }
    }
}
