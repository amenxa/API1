using Microsoft.EntityFrameworkCore;

namespace ApiTest.Data
{
    public class AppDbContext : DbContext 
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {
            
        }

        public DbSet<Item>Items { get; set; }
        public DbSet<Category>Categories { get; set; }


    }
}
