using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Testing.RestApi.Models;

namespace Testing.RestApi.Controllers
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
                BlogListResponseModel model = new BlogListResponseModel
                {
                    IsSuccess = true,
                    Message = "success",
                    Data = lst
                };
                return Ok(model);
            }
            catch (Exception ex)
            {
                return Ok(new BlogListResponseModel
                {
                    IsSuccess = false,
                    Message = ex.ToString(),//summary error
                                            //Message = ex.Message(), //detail error
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
                var response = new { isSuccess = false, Message = "no data found" };
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
                Message = result > 0 ? "create successful" : "create fail",
                Data = blog,
            };
            return Ok(model);
        }

        [HttpPut("{id}")]
        public IActionResult PutBlog(int id, BlogDataModel blog)
        {
            AppDBContext db = new AppDBContext();
            var item = db.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if (item is null)
            {
                var response = new { isSuccess = false, Message = "no data found" };
                return NotFound(response);
            }
            item.Blog_Title = blog.Blog_Title;
            item.Blog_Author = blog.Blog_Author;
            item.Blog_Content = blog.Blog_Content;

            var result = db.SaveChanges();
            BlogResponseModel model = new BlogResponseModel()
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Update successful" : "update fail",
                Data = item
            };
            return Ok(model);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogDataModel blog)
        {
            AppDBContext db = new AppDBContext();
            var item = db.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if (item is null)
            {
                var response = new { isSuccess = false, Message = "no data found" };
                return NotFound(response);
            }
            if (!string.IsNullOrEmpty(blog.Blog_Title))
            {
                item.Blog_Title = blog.Blog_Title;
            }
            if (!string.IsNullOrEmpty(blog.Blog_Author))
            {
                item.Blog_Author = blog.Blog_Author;
            }
            if (!string.IsNullOrEmpty(blog.Blog_Content))
            {
                item.Blog_Content = blog.Blog_Content;
            }
            var result = db.SaveChanges();
            BlogResponseModel model = new BlogResponseModel()
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "patch successful" : "patch fail",
                Data = item,
            };
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            AppDBContext db = new AppDBContext();
            var item = db.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if(item is null)
            {
                var response = new { isSuccess = false, Message = "no data found" };
                return NotFound(response);
            }
            db.Blogs.Remove(item);
            var result = db.SaveChanges();
            BlogResponseModel model = new BlogResponseModel()
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "delete successful" : "delete fail",
            };
            return Ok(model);
        }
    }
}
