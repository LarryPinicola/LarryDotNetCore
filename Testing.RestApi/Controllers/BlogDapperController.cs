using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata;
using Testing.RestApi.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Testing.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogDapperController : ControllerBase
    {
        private readonly SqlConnectionStringBuilder sqlConnectionStringBuilder;

        public BlogDapperController()
        {
            sqlConnectionStringBuilder = new SqlConnectionStringBuilder
            {
                DataSource = ".",
                InitialCatalog = "LarryDotNetCore",
                UserID = "sa",
                Password = "sa@123"
            };
        }

        [HttpGet]
        public IActionResult GetBlogs()
        {
            try
            {
                string query = "SELECT * FROM Tbl_Blog";
                using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
                List<BlogDataModel> lst = db.Query<BlogDataModel>(query).ToList();
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
            string query = "SELECT * FROM Tbl_Blog WHERE Blog_Id = @Blog_Id";
            using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            BlogDataModel? item = db.Query<BlogDataModel>(query, new BlogDataModel { Blog_Id = id }).FirstOrDefault();
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
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([Blog_Title]
           ,[Blog_Author]
           ,[Blog_Content])
     VALUES
           (@Blog_Title,@Blog_Author,@Blog_Content)";
            using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blog);

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
            string query = "SELECT * FROM Tbl_Blog WHERE Blog_Id = @Blog_Id";
            using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            BlogDataModel? item = db.Query<BlogDataModel>(query, new BlogDataModel { Blog_Id = id }).FirstOrDefault();
            if (item is null)
            {
                var response = new { isSuccess = false, Message = "no data found" };
                return NotFound(response);
            }

            query = @"UPDATE [dbo].[Tbl_Blog]
   SET [Blog_Title] =@Blog_Title
      ,[Blog_Author] = @Blog_Author
      ,[Blog_Content] = @Blog_Content
 WHERE Blog_Id = @Blog_Id";
            using IDbConnection db2 = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            int result = db2.Execute(query, blog);

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
            string query = "SELECT * FROM Tbl_Blog WHERE Blog_Id = @Blog_Id";
            using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            BlogDataModel? item = db.Query<BlogDataModel>(query, new BlogDataModel { Blog_Id = id }).FirstOrDefault();

            if (item is null)
            {
                var response = new { isSuccess = false, Message = "no data found" };
                return NotFound(response);
            }

            string conditions = "";

            if (!string.IsNullOrEmpty(blog.Blog_Title))
            {
                conditions += "[Blog_Title] =@Blog_Title, ";
                item.Blog_Title = blog.Blog_Title;
            }
            if (!string.IsNullOrEmpty(blog.Blog_Author))
            {
                conditions += "[Blog_Author] = @Blog_Author, ";
                item.Blog_Author = blog.Blog_Author;
            }
            if (!string.IsNullOrEmpty(blog.Blog_Content))
            {
                conditions += "[Blog_Content] = @Blog_Content, ";
                item.Blog_Content = blog.Blog_Content;
            }

            if (conditions.Length == 0)
            {
                var response = new { isSuccess = false, Message = "no data to update" };
                return NotFound(response);
            }

            conditions = conditions.Substring(0, conditions.Length - 2);

            query = $@"UPDATE [dbo].[Tbl_Blog]
                        SET {conditions}
                    WHERE Blog_Id = @Blog_Id";

            using IDbConnection db2 = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            int result = db2.Execute(query, blog);

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
            string query = "SELECT * FROM Tbl_Blog WHERE Blog_Id = @Blog_Id";
            using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            BlogDataModel? item = db.Query<BlogDataModel>(query, new BlogDataModel { Blog_Id = id }).FirstOrDefault();
            if (item is null)
            {
                var response = new { isSuccess = false, Message = "no data found" };
                return NotFound(response);
            }
            query = @"DELETE FROM [dbo].[Tbl_Blog]
      WHERE Blog_Id = @Blog_Id";
            using IDbConnection db2 = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            int result = db2.Execute(query, new BlogDataModel { Blog_Id = id });
            BlogResponseModel model = new BlogResponseModel()
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "delete successful" : "delete fail",
            };
            return Ok(model);
        }
    }
}
