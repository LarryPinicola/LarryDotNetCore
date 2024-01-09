using LarryDotNetCore.SessionWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LarryDotNetCore.SessionWebApp.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            var str = HttpContext.Session.GetString("LoginData");
            if (str != null)
            {
                return Redirect("/home");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginViewModel reqModel)
        {
            //Logic

            HttpContext.Session.SetString("LoginData", JsonConvert.SerializeObject(reqModel));
            return Redirect("/");
        }
    }
}
