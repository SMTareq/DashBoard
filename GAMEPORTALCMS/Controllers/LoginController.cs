using GAMEPORTALCMS.Models.Response;
using GAMEPORTALCMS.Repository.Implementation;
using Microsoft.AspNetCore.Mvc;

namespace GAMEPORTALCMS.Controllers
{
    public class LoginController : Controller
    {
        UserRepository _userrepository;
        public LoginController(UserRepository repo)
        {
            _userrepository = repo;
        }
        public IActionResult Index()
        {

            return View();
        }

        public async Task<IActionResult> ValidateUser(string userName,string password)
        {
            //var data = await _userrepository.ValidateUser(userName, password);
            //if(data == null)
            //{
            //    return new JsonResult(new ResponseModel { Success = false, Message = "Username and password do not match" });
            //}
            //else
            //{
            //    HttpContext.Session.SetString("UserName", data.UserCode);
            //    ViewBag.UserName = data.Username;
            //    return new JsonResult(new ResponseModel { Success = true, Message = "Login successfull" });
            //}
            HttpContext.Session.SetString("UserName", "admin");
            ViewBag.UserName = "admin";
            return new JsonResult(new ResponseModel { Success = true, Message = "Login successfull" });
        }
    }
}
