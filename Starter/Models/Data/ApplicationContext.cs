using Microsoft.EntityFrameworkCore;

namespace Starter.Models.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Record> Records { get; set; }
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=starter1;Trusted_Connection=True;");
        }
    }
}
