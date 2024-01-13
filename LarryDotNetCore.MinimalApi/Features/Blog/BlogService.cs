using LarryDotNetCore.MinimalApi;
using LarryDotNetCore.MinimalApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LarryDotNetCore.MinimalApi.Features.Blog
{
    public static class BlogService
    {
        public static void AddBlogService(this IEndpointRouteBuilder app)
        {
            // ReadBlogs
            app.MapGet("/blog/{pageNo}/{pageSize}", async ([FromServices] AppDBContext db, int pageNo, int pageSize) =>
            {
                return await db.Blogs
                .AsNoTracking()
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            })
                .WithName("GetBlogs")
                .WithOpenApi();

            // CreateBlogs
            app.MapPost("/blog", async ([FromServices] AppDBContext db, BlogDataModel blog) =>
            {
                await db.Blogs.AddAsync(blog);
                int result = await db.SaveChangesAsync();
                string message = result > 0 ? "Saving Successful." : "Saving Failed";
                return Results.Ok(new BlogResponseModel
                {
                    Data = blog,
                    Message = message,
                    IsSuccess = result > 0,
                });
            })
                .WithName("CreateBlog")
                .WithOpenApi();

            // UpdateBlog
            app.MapPut("/blog/{id}", async ([FromServices] AppDBContext db, int id, BlogDataModel blog) =>
            {
                var item = await db.Blogs.FirstOrDefaultAsync(i => i.Blog_Id == id);
                if (item == null)
                {
                    return Results.NotFound(
                        new
                        {
                            Message = "No Data Found",
                            IsSuccess = false,
                        });
                }
                item.Blog_Title = blog.Blog_Title;
                item.Blog_Author = blog.Blog_Author;
                item.Blog_Content = blog.Blog_Content;

                var result = db.SaveChanges();
                BlogResponseModel model = new BlogResponseModel
                {
                    IsSuccess = result > 0,
                    Message = result > 0 ? "Update Successful" : "Update Failed",
                    Data = item
                };
                return Results.Ok(model);
            })
                .WithName("UpdateBlog")
                .WithOpenApi();


            // PatchBlog
            app.MapPatch("/blog/{id}", async ([FromServices] AppDBContext db, int id, BlogDataModel blog) =>
            {
                var item = db.Blogs.FirstOrDefault(x => x.Blog_Id == id);
                if (item == null)
                {
                    var response = new { IsSuccess = false, Message = "No Data Found." };
                    return Results.NotFound(response);
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
                BlogResponseModel model = new BlogResponseModel
                {
                    IsSuccess = result > 0,
                    Message = result > 0 ? "Updating Successful." : "Updating Failed",
                    Data = item
                };
                return Results.Ok(model);
            })
                .WithName("PatchBlog")
                .WithOpenApi();

            // DeleteBlog
            app.MapDelete("/blog/{id}", ([FromServices] AppDBContext db, int id) =>
            {
                var item = db.Blogs.FirstOrDefault(x => x.Blog_Id == id);
                if (item is null)
                {
                    var response = new { IsSuccess = false, Message = "No data Found." };
                    return Results.NotFound(response);
                }
                db.Blogs.Remove(item);
                var result = db.SaveChanges();
                BlogResponseModel model = new BlogResponseModel
                {
                    IsSuccess = result > 0,
                    Message = result > 0 ? "Deleting Successful" : "Delete Failed."
                };
                return Results.Ok(model);
            })
                .WithName("DeleteBlog")
                .WithOpenApi();
        }
    }
}