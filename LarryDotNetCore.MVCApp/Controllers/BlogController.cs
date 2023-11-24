using LarryDotNetCore.MVCApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace LarryDotNetCore.MVCApp.Controllers
{
    public class BlogController : Controller
    {
        [ActionName("Index")] //index refers to blogIndex, but by giving ACTIONNAME, we can only searched by index on url
        public IActionResult BlogIndex()
        {
            AppDBContext db = new AppDBContext();
            List<BlogDataModel> lst = db.Blogs.ToList();
            return View("BlogIndex", lst); // MVC will find index, but here we retrun that index as BlogIndex
        }
    }
}
