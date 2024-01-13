using LarryDotNetCore.MVCApp.Interfaces;
using LarryDotNetCore.MVCApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace LarryDotNetCore.MVCApp.Controllers
{
    public class BlogRefitController : Controller
    {
        private readonly IBlogApi _blogApi;

        public BlogRefitController(IBlogApi blogApi)
        {
            _blogApi = blogApi;
        }

        [ActionName("Index")]
        public async Task<IActionResult> Index()
        {
            var model = await _blogApi.GetBlogs();
            return View(model);
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
            var model = await _blogApi.CreateBlog(reqModel);
            return Redirect("/blogrefit");
        }

        [ActionName("Edit")]
        public async Task<IActionResult> BlogEdit(int id)
        {
            var model = await _blogApi.GetBlog(id);
            return View("BlogEdit", model);
        }

        [HttpPost]
        [ActionName("Update")]
        public async Task<IActionResult> BlogUpdate(int id, BlogDataModel reqModel)
        {
            var model = await _blogApi.UpdateBlog(id, reqModel);
            return Redirect("/blogrefit");
        }

        [ActionName("Delete")]
        public async Task<IActionResult> BlogDelete(int id)
        {
            var model = await _blogApi.DeleteBlog(id);
            return Redirect("/blogrefit");
        }
    }
}
