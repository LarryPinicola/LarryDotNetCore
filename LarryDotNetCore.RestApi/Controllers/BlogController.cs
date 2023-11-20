using LarryDotNetCore.RestApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LarryDotNetCore.RestApi.Controllers
{
    // api/blog
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetBlogs()
        {
            try
            {
                AppDBContext db = new AppDBContext();
                List<BlogDataModel> lst = db.Blogs.ToList();

                /*BlogListResponseModel model = new BlogListResponseModel();
                model.IsSuccess = true;
                model.Message = "success";
                model.Data = lst;*/
                //return Ok("GetBlogs");

                BlogListResponseModel model = new BlogListResponseModel
                {
                    IsSuccess = true,
                    Message = "success",
                    Data = lst,
                };
                return Ok(model);
            }
            catch (Exception ex)
            {

                return Ok(new BlogListResponseModel
                {
                    IsSuccess = false,
                    //Message = ex.ToString() //detail error message
                    Message = ex.Message, //summery error message
                });
            }

        }

        [HttpGet("{id}")]
        public IActionResult EditBlog(int id)
        {
            AppDBContext db = new AppDBContext();
            var item = db.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if (item is null)
            {
                var response = new { IsSuccess = false, Message = "No data found" };
                return NotFound(response);
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreateBlog(BlogDataModel blog)
        {
            AppDBContext db = new AppDBContext();
            db.Blogs.Add(blog);
            var result = db.SaveChanges();
            BlogResponseModel model = new BlogResponseModel()
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "saving successful." : "saving fail.",
                Data = blog,
            };
            return Ok(model);
        }

        [HttpPut]
        public IActionResult UpdateBlog()
        {
            return Ok("UpdateBlog");
        }

        [HttpPatch]
        public IActionResult PatchBlog()
        {
            return Ok("PatchBlog");
        }

        [HttpDelete]
        public IActionResult DeleteBlog()
        {
            return Ok("DeleteBlog");
        }
    }
}
