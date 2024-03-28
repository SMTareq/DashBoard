using GAMEPORTALCMS.Models.Response;
using GAMEPORTALCMS.Repository.Implementation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GAMEPORTALCMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EBLLoginController : ControllerBase
    {
        UserRepository _userrepository;

        public EBLLoginController(UserRepository repo)
        {
            _userrepository = repo;
        }

        [HttpPost("UserLogin")]
        public async Task<IActionResult> UserLogin(string userName, string password)
        {
            var data = await _userrepository.ValidateUser(userName, password);
            if (data == null)
            {
                return new JsonResult(new ResponseModel { Success = false, Message = "Username and password do not match" });
            }
            else
            {
                HttpContext.Session.SetString("UserName", data.name);
              //  ViewBag.UserName = data.name;
                return new JsonResult(new ResponseModel { Success = true, Message = "Login successfull" });
            }
       
        }

    }
}
