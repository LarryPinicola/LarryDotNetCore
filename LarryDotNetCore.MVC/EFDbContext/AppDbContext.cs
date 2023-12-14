using LarryDotNetCore.MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace LarryDotNetCore.MVC.EFDbContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BlogDataModel> Blogs { get; set; }
    }
}
