using Microsoft.AspNetCore.Mvc;

namespace LarryDotNetCore.MVCApp.Controllers
{
    public class UserController : Controller
    {
        [ActionName("index")]
        public IActionResult UserIndex()
        {
            return View("UserIndex");
        }
    }
}
