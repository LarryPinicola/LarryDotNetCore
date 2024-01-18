using LarryDotNetCore.ATMWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LarryDotNetCore.ATMWebApp.EfDbContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<AtmDataModel> AtmData { get; set; }
    }
}
