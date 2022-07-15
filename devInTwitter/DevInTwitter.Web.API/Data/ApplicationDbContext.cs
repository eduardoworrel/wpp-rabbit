using Microsoft.EntityFrameworkCore;
using Models;
namespace Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        { }
        
        public DbSet<Post> Post { get; set; }
    }
}