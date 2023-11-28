using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Testing.RestApi.Models;

namespace Testing.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoController : ControllerBase
    {
        private readonly SqlConnectionStringBuilder sqlConnectionStringBuilder;

        public BlogAdoController()
        {
            sqlConnectionStringBuilder = new SqlConnectionStringBuilder
            {
                DataSource = ".",
                InitialCatalog = "LarryDotNetCore",
                UserID = "sa",
                Password = "sa@123",
            };
        }

        [HttpGet]
        public IActionResult GetBlogs()
        {
            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            string query = "SELECT * FROM tbl_blog";
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            connection.Close();

            List<BlogDataModel> lst = new List<BlogDataModel>();
            foreach (DataRow row in dt.Rows)
            {
                BlogDataModel item = new BlogDataModel();
                item.Blog_Id = Convert.ToInt32(row["blog_id"]);
                item.Blog_Title = Convert.ToString(row["blog_title"]);
                item.Blog_Author = Convert.ToString(row["blog_authour"]);
                item.Blog_Content = Convert.ToString(row["blog_content"]);
                lst.Add(item);
            }

            BlogListResponseModel model = new BlogListResponseModel()
            {
                IsSuccess = true,
                Message = "Success",
                Data = lst
            };
            return Ok(model);
        }


        [HttpGet("{id}")]
        public IActionResult EditBlog(int id)
        {
            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            string query = "SELECT * FROM Tbl_Blog WHERE Blog_Id = @Blog_Id";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Blog_Id", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();

            List<BlogDataModel> lst = new List<BlogDataModel>();
            if (dt.Rows.Count == 0)
            {
                var response = new { isSuccess = false, Message = "no data found" };
                return NotFound(response);
            }

            DataRow row = dt.Rows[0];
            BlogDataModel item = new BlogDataModel
            {
                Blog_Id = Convert.ToInt32(row["Blog_Id"]),
                Blog_Title = row["Blog_Title"].ToString(),
                Blog_Author = row["Blog_Author"].ToString(),
                Blog_Content = row["Blog_Content"].ToString(),
            };

            BlogListResponseModel model = new BlogListResponseModel()
            {
                IsSuccess = true,
                Message = "success",
                Data = lst
            };
            return Ok(model);
        }

        [HttpPost]
        public IActionResult CreateBlog(BlogDataModel blog)
        {
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
                           ([Blog_Title]
                           ,[Blog_Author]
                           ,[Blog_Content])
                            VALUES (@Blog_Title ,@Blog_Author ,@Blog_Content)";
            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Blog_Title", blog.Blog_Title);
            cmd.Parameters.AddWithValue("@Blog_Author", blog.Blog_Author);
            cmd.Parameters.AddWithValue("@Blog_Content", blog.Blog_Content);
            int result = cmd.ExecuteNonQuery();
            connection.Close();

            BlogResponseModel model = new BlogResponseModel()
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "created successful" : "create failed",
                Data = blog
            };
            return Ok(model);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogDataModel blog)
        {
            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            string query = "SELECT * FROM tbl_blog WHERE Blog_Id = @Blog_Id";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Blog_Id", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();

            if (dt.Rows.Count == 0)
            {
                var response = new { isSuccess = false, Message = "no data found" };
                return NotFound(response);
            }

            connection.Open();
            query = @"UPDATE [dbo].[Tbl_Blog]
                    SET [Blog_Title] = @Blog_Title
                        ,[Blog_Author] = @Blog_Author
                        ,[Blog_Content] = @Blog_Content
                    WHERE Blog_Id = @Blog_Id";
            cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Blog_Id", id);
            cmd.Parameters.AddWithValue("@Blog_Title", blog.Blog_Title);
            cmd.Parameters.AddWithValue("@Blog_Author", blog.Blog_Author);
            cmd.Parameters.AddWithValue("@Blog_Content", blog.Blog_Content);
            int result = cmd.ExecuteNonQuery();
            connection.Close();

            BlogResponseModel model = new BlogResponseModel()
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "updated successful" : "update failed",
                Data = blog
            };
            return Ok(model);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogDataModel blog)
        {
            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            string query = "SELECT * FROM tbl_blog WHERE blog_id = @blog_id";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Blog_Id", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();

            if (dt.Rows.Count == 0)
            {
                var response = new { isSuccess = false, Message = "no data found" };
                return NotFound(response);
            }

            string conditions = "";
            SqlConnection connection2 = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection2.Open();
            SqlCommand cmd2 = new SqlCommand(query, connection);

            if (!string.IsNullOrEmpty(blog.Blog_Title))
            {
                conditions += "[Blog_Title] = @Blog_Title, ";
                cmd2.Parameters.AddWithValue("@Blog_Title", blog.Blog_Title);
            }
            if (!string.IsNullOrEmpty(blog.Blog_Author))
            {
                conditions += "[Blog_Author] = @Blog_Author, ";
                cmd2.Parameters.AddWithValue("@Blog_Author", blog.Blog_Author);
            }
            if (!string.IsNullOrEmpty(blog.Blog_Content))
            {
                conditions += "[Blog_Content] = @Blog_Content, ";
                cmd2.Parameters.AddWithValue("@Blog_Content", blog.Blog_Content);
            }
            if (conditions.Length > 0)
            {
                conditions = conditions.Substring(0, conditions.Length - 2);
            }
            string queryUpdate = $@"UPDATE [dbo].[Tbl_Blog] SET {conditions} WHERE Blog_Id = @Blog_Id";
            cmd2.Parameters.AddWithValue("@Blog_Id", id);
            var result = cmd2.ExecuteNonQuery();
            connection2.Close();
            BlogResponseModel model = new BlogResponseModel()
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Patched successful" : "patch failed",
                //Data = blog,
            };
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            string query = @"DELETE FROM [dbo].[Tbl_Blog] WHERE Blog_Id = @Blog_Id";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Blog_Id", id);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            int result = cmd.ExecuteNonQuery();
            connection.Close();

            BlogResponseModel model = new BlogResponseModel()
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "deleted successful" : "delete failed",
            };
            return Ok(model);
        }
    }
}
