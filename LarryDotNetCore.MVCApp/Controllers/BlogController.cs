using LarryDotNetCore.MVCApp.EFDbContext;
using LarryDotNetCore.MVCApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LarryDotNetCore.MVCApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDBContext _context;

        public BlogController(AppDBContext dbContext)
        {
            _context = dbContext;
        }

        // GET - List
        [ActionName("Index")] //index refers to blogIndex, but by giving ACTIONNAME, we can only searched by index on url
        public IActionResult BlogIndex()
        {
            List<BlogDataModel> lst = _context.Blogs.ToList();
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
            await _context.Blogs.AddAsync(reqModel);
            var result = await _context.SaveChangesAsync();
            string message = result > 0 ? "Saving successful" : "Saving failed";
            TempData["Message"] = message;
            TempData["IsSuccess"] = result > 0;
            return Redirect("/blog");
        }

        // blog/edit/1
        [ActionName("Edit")]
        public async Task<IActionResult> BlogEdit(int id)
        {
            if (!await _context.Blogs.AsNoTracking().AnyAsync(x => x.Blog_Id == id))
            {
                string message = "No data found";
                TempData["Message"] = message;
                TempData["IsSuccess"] = false;
                return Redirect("/blog");
            }
            var blog = await _context.Blogs.AsNoTracking().FirstOrDefaultAsync(x => x.Blog_Id == id);
            if (blog is null)
            {
                TempData["Message"] = "No data found";
                TempData["IsSuccess"] = false;
                return Redirect("/blog");
            }
            return View("BlogEdit", blog);
        }

        [HttpPost]
        [ActionName("Update")]
        public async Task<IActionResult> BlogUpdate(int id, BlogDataModel reqModel)
        {
            if (!await _context.Blogs.AsNoTracking().AnyAsync(x => x.Blog_Id == id))
            {
                TempData["Message"] = "No data found";
                TempData["IsSuccess"] = false;
                return Redirect("/blog");
            }

            var blog = await _context.Blogs.FirstOrDefaultAsync(x => x.Blog_Id == id);
            if (blog is null)
            {
                TempData["Message"] = "No data found";
                TempData["IsSuccess"] = false;
                return Redirect("/blog");
            }
            blog.Blog_Title = reqModel.Blog_Title;
            blog.Blog_Author = reqModel.Blog_Author;
            blog.Blog_Content = reqModel.Blog_Content;

            int result = _context.SaveChanges();
            string message = result > 0 ? "Updating successful" : "Update Failed";
            TempData["Message"] = message;
            TempData["IsSuccess"] = result > 0;

            return Redirect("/blog");
        }

        [ActionName("Delete")]
        public async Task<IActionResult> BlogDelete(int id)
        {
            if (!await _context.Blogs.AsNoTracking().AnyAsync(x => x.Blog_Id == id))
            {
                TempData["Message"] = "No data Found";
                TempData["IsSuccess"] = false;
                return Redirect("/blog");
            }

            var blog = await _context.Blogs.AsNoTracking().FirstOrDefaultAsync(x => x.Blog_Id == id);
            if (blog is null)
            {
                TempData["Message"] = "No data Found";
                TempData["IsSuccess"] = false;
                return Redirect("/blog");
            }

            _context.Remove(blog);
            int result = _context.SaveChanges();
            string message = result > 0 ? "Deleting Successful" : "Deleted Failed";
            TempData["Message"] = message;
            TempData["IsSuccess"] = result > 0;

            return Redirect("/blog");
        }
    }
}
