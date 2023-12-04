using Microsoft.AspNetCore.Mvc;
using TestingDotNetCore.MVCApp.Models;

namespace TestingDotNetCore.MVCApp.Controllers
{
    public class BlogController : Controller
    {
        [ActionName("Index")]
        public IActionResult BlogIndex()
        {
            AppDBContext db = new AppDBContext();
            List<BlogDataModel> lst = db.Blogs.ToList();
            return View("BlogIndex", lst);
        }
    }
}
