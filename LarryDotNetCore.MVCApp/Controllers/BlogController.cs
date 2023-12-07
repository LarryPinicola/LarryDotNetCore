using LarryDotNetCore.MVCApp.EFDbContext;
using LarryDotNetCore.MVCApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace LarryDotNetCore.MVCApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDBContext _dbContext;

        public BlogController(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET - List
        [ActionName("Index")] //index refers to blogIndex, but by giving ACTIONNAME, we can only searched by index on url
        public IActionResult BlogIndex()
        {
            List<BlogDataModel> lst = _dbContext.Blogs.ToList();
            return View("BlogIndex", lst); // MVC will find index, but here we retrun that index as BlogIndex
        }

        [ActionName("Create")]
        public IActionResult BlogCreate()
        {
            return View("BlogCreate");
        }

        [HttpPost]
        [ActionName("Save")]
        public async Task<IActionResult> BlogSave(BlogDataModel reqModel)
        {
            await _dbContext.Blogs.AddAsync(reqModel);
            var result = await _dbContext.SaveChangesAsync();
            string message = result > 0 ? "Saving successful" : "Saving failed";
            TempData["Message"] = message;
            TempData["IsSuccess"] = result > 0;
            return Redirect("/blog");
        }
    }
}
