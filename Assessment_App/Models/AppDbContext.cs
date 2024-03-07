using Microsoft.EntityFrameworkCore;

namespace Assessment_App.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
