using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using System.Security.Claims;

namespace GAMEPORTALCMS.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
                
        }

        protected string UserName
        {
            get
            {
                return HttpContext.Session.GetString("UserName");
            }
            set
            {
                HttpContext.Session.SetString("UserName", value);
            }
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (string.IsNullOrEmpty(UserName))
            {
                context.Result = new RedirectToActionResult("Index", "Login", null);
            }
            else
            {
                ViewBag.UserName = UserName;
            }

            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }
    }
}
