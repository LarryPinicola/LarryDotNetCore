using LarryDotNetCore.SessionWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LarryDotNetCore.SessionWebApp.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginViewModel reqModel)
        {
            //Logic

            HttpContext.Session.SetString("LoginData", JsonConvert.SerializeObject(reqModel));
            return Redirect("/home");
        }
    }
}
