using LarryDotNetCore.MVCApp.EFDbContext;
using LarryDotNetCore.MVCApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LarryDotNetCore.MVCApp.Controllers
{
    public class BlogAjaxController : Controller
    {
        private readonly AppDBContext _context;

        public BlogAjaxController(AppDBContext contexxt)
        {
            _context = contexxt;
        }

        [ActionName("List")]
        public async Task<IActionResult> BlogList(int pageNo = 1, int pageSize = 10)
        {
            BlogDataResponseModel model = new BlogDataResponseModel();
            List<BlogDataModel> lst = _context.Blogs.AsNoTracking()
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize).ToList();

            int rowCount = await _context.Blogs.CountAsync();
            int pageCount = rowCount / pageSize;
            if (rowCount % pageSize == 0)
            {
                pageCount++;
            }

            model.Blogs = lst;
            model.PageSetting = new PageSettingModel(pageNo, pageSize, pageCount, "/blog/list");

            return View("BlogList", model);
        }

        [HttpPost]
        [ActionName("Create")]
        public IActionResult BlogCreate()
        {
            return View("BlogCreate");
        }

        [HttpPost]
        [ActionName("Save")]
        public async Task<IActionResult> BlogSave(BlogDataModel reqModel)
        {

            return Redirect("/blog");
        }
    }
}
