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
                var currentBlog = await db.Blogs.FindAsync(id);
                if (currentBlog == null)
                {
                    return Results.NotFound("Data Not Found");
                }
                int result = await db.SaveChangesAsync();
                string message = result > 0 ? "Update Successful" : "Update Failed";
                return Results.Ok(new BlogResponseModel
                { Data = blog, Message = message, IsSuccess = result > 0, });
            })
                .WithName("UpdateBlog")
                .WithOpenApi();
        }
    }
}
