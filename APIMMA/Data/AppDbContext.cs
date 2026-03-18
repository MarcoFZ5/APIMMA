using APIMMA.Models;
using Microsoft.EntityFrameworkCore;

namespace APIMMA.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<"Entity">()
            //    .Implementations

            // TO - DO : Declare relationships and constraints here if needed

           
            
        }
    }  
}
