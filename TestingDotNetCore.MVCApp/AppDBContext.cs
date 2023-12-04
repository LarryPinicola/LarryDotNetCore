using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TestingDotNetCore.MVCApp.Models;

namespace TestingDotNetCore.MVCApp
{
    public class AppDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var sqlConnectionStringBuilder = new SqlConnectionStringBuilder
                {
                    DataSource = ".",
                    InitialCatalog = "LarryDotNetCore",
                    UserID = "sa",
                    Password = "sa@123",
                    TrustServerCertificate = true
                };
                optionsBuilder.UseSqlServer(sqlConnectionStringBuilder.ConnectionString);
            }
        }

        public DbSet<BlogDataModel> Blogs { get; set; }
    }
}
