using GAMEPORTALCMS.Models.Entity;
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
        MailGenerator _mail;



        public EBLLoginController(UserRepository repo, MailGenerator mail)
        {
            _userrepository = repo;
            _mail = mail;
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
                // ViewBag.UserName = data.name;
                return new JsonResult(new ResponseModel { Success = true, Message = "Login successfull" });
            }
       
        }

        [HttpGet("EBLEmployeeInfo")]
        public async Task<IActionResult> Get_EBL_Employee_Info()
        {
            try
            {
                var data = await _userrepository.Get_EBL_UserMail_All(); 
                return Ok(data);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        //Mail_Confrigation

     


    }
}
