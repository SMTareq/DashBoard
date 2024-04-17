using GAMEPORTALCMS.Models.DTO;
using GAMEPORTALCMS.Models.Entity;
using GAMEPORTALCMS.Models.Response;
using GAMEPORTALCMS.Repository.Implementation;
using Microsoft.AspNetCore.Mvc;

namespace GAMEPORTALCMS.Controllers
{
    public class LoginController : Controller
    {
        UserRepository _userrepository;

        private readonly MailGenerator _mail;


        public LoginController(UserRepository repo, MailGenerator mail)
        {
            _userrepository = repo;
            _mail = mail;
        }
        public IActionResult Index()
        {

            return View();
        }

        public async Task<IActionResult> ValidateUser(string userName, string password)
        {
            var data = await _userrepository.ValidateUser(userName, password);
            if (data == null)
            {
                return new JsonResult(new ResponseModel { Success = false, Message = "Username and password do not match" });
            }
            else
            {
                HttpContext.Session.SetString("UserName", data.name);
                ViewBag.UserName = data.name;
                return new JsonResult(new ResponseModel { Success = true, Message = "Login successfull" });
            }
            //if (userName == "" && password == "")
            //{
            //    return new JsonResult(new ResponseModel { Success = false, Message = "Username and password do not match" });
            //}
            //else
            //{

            //    if (userName == "admin" && password == "admin")
            //    {                  
            //        HttpContext.Session.SetString("UserName", "admin");
            //        ViewBag.UserName = "admin";
            //        return new JsonResult(new ResponseModel { Success = true, Message = "Login successfull" });
            //    }
            //    else
            //    {
            //        return new JsonResult(new ResponseModel { Success = false, Message = "Username and password do not match" });
            //    }
            //}
        }

        public async Task<IActionResult> Mailsend(MailSendDTO jsonData)
        {
            
            bool data = await _mail.SendMail(jsonData);
            if (data)
            {
                return new JsonResult(new ResponseModel { Success = true, Message = "Mail sent successfully" });
            }
            else
            {
                return new JsonResult(new ResponseModel { Success = false, Message = "error" });
            }
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
                ViewBag.UserName = data.name;
                return new JsonResult(new ResponseModel { Success = true, Message = "Login successfull" });
            }
            //if (userName == "" && password == "")
            //{
            //    return new JsonResult(new ResponseModel { Success = false, Message = "Username and password do not match" });
            //}
            //else
            //{

            //    if (userName == "admin" && password == "admin")
            //    {                  
            //        HttpContext.Session.SetString("UserName", "admin");
            //        ViewBag.UserName = "admin";
            //        return new JsonResult(new ResponseModel { Success = true, Message = "Login successfull" });
            //    }
            //    else
            //    {
            //        return new JsonResult(new ResponseModel { Success = false, Message = "Username and password do not match" });
            //    }
            //}
        }

    }
}
