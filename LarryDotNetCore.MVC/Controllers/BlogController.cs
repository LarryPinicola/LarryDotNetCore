using LarryDotNetCore.MVC.EFDbContext;
using LarryDotNetCore.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace LarryDotNetCore.MVC.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;

        public BlogController(AppDbContext context)
        {
            _context = context;
        }

        // GET / List
        [ActionName("Index")]
        public IActionResult BlogIndex()
        {
            List<BlogDataModel> lst = _context.Blogs.ToList();
            return View("BlogIndex", lst);
        }

        // CREATE 
        [ActionName("Create")]
        public IActionResult BlogCreate()
        {
            return View("BlogCreate");
        }

        [HttpPost]
        [ActionName("Save")]
        public async Task<IActionResult> BlogSave(BlogDataModel reqModel)
        {
            await _context.Blogs.AddAsync(reqModel);
            var result = await _context.SaveChangesAsync();
            TempData["Message"] = result > 0 ? "Saving Successful" : "Saving Failed";
            TempData["IsSuccess"] = result > 0;
            return Redirect("/blog");
        }


    }
}
