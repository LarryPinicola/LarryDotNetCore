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
                Message = result > 0 ? "successfully created" : "craete fail.",
                Data = blog,
            };
            return Ok(model);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogDataModel blog)
        {
            AppDBContext db = new AppDBContext();
            var item = db.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if (item is null)
            {
                var response = new { isSuccess = false, Message = "No data found" };
                return NotFound(response);
            }
            item.Blog_Title = blog.Blog_Title;
            item.Blog_Author = blog.Blog_Author;
            item.Blog_Content = blog.Blog_Content;
            var result = db.SaveChanges();
            BlogResponseModel model = new BlogResponseModel()
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "sussessfully updated" : "Update Failed",
                Data = item,
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
                var response = new { isSuccess = false, Message = "No data Found" };
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
            //item.Blog_Id = blog.Blog_Id;
            BlogResponseModel model = new BlogResponseModel()
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Update successful" : "update failed",
                Data = item,
            };
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            AppDBContext db = new AppDBContext();
            var item = db.Blogs.FirstOrDefault(y => y.Blog_Id == id);
            if (item is null)
            {
                var response = new { isSuccess = false, Message = "No Data Found" };
                return NotFound(response);
            }
            db.Blogs.Remove(item);
            var result = db.SaveChanges();
            BlogResponseModel model = new BlogResponseModel()
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "delete successful" : "delete failed",
                Data = item,
            };
            return Ok(model);
        }
    }
}