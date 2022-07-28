using Microsoft.EntityFrameworkCore;
using System.Configuration;

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
            optionsBuilder.UseSqlServer(ConfigurationManager.AppSettings["SqlServerConnection"]);
        }
    }
}
