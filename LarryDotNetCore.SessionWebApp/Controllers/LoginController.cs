using LarryDotNetCore.SessionWebApp.Models;
using Microsoft.AspNetCore.Mvc;

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

            return View();
        }
    }
}
