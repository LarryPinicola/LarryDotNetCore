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
        public IActionResult PostBlog()
        {
            return Ok("PostBlog");
        }

        [HttpPut]
        public IActionResult PutBlog()
        {
            return Ok("PutBlog");
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
