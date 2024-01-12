using LarryDotNetCore.MinimalApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LarryDotNetCore.MinimalApi
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BlogDataModel> Blogs { get; set; }
    }
}
