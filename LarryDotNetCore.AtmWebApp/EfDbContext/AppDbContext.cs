using LarryDotNetCore.AtmWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LarryDotNetCore.AtmWebApp.EfDbContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<AtmDataModel> AtmData { get; set; }
    }
}
