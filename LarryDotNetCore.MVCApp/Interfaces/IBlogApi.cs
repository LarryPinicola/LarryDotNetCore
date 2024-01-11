//using LarryDotNetCore.RestApi.Models;
using LarryDotNetCore.MVCApp.Models;
using Refit;

namespace LarryDotNetCore.MVCApp.Interfaces
{
    public interface IBlogApi
    {
        [Get("/api/blog")]
        Task<BlogListResponseModel> GetBlogs();

        [Get("/api/blog/{id}")]
        Task<BlogResponseModel> GetBlog(int id);

        [Post("/api/blog/")]
        Task<BlogResponseModel> CreateBlog(BlogDataModel blog);

        [Put("/api/blog/{id}")]
        Task<BlogResponseModel> UpdateBlog(int id, BlogDataModel blog);

        [Delete("api/blog/{id}")]
        Task<BlogResponseModel> DeleteBlog(int id);
    }
}
