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
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Classes> Classes { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<"Entity">()
            //    .Implementations

            // TO - DO : Declare relationships and constraints here if needed

            modelBuilder.Entity<Post>()
                .HasOne(post => post.User)
                .WithMany(user => user.Posts)
                .HasForeignKey(post => post.User_id); 

            modelBuilder.Entity<Comment>()
                .HasOne(comment => comment.User)
                .WithMany(user => user.Comments)
                .HasForeignKey(comment => comment.User_id);

            modelBuilder.Entity<Comment>()
                .HasOne(comment => comment.Post)
                .WithMany(post => post.Comments)
                .HasForeignKey(comment => comment.Post_id);

            modelBuilder.Entity<Membership>()
                .HasOne(membership => membership.User)
                .WithMany(user => user.Memberships)
                .HasForeignKey(membership => membership.User_id);

            modelBuilder.Entity<Payment>()
                .HasOne(payment => payment.Membership)
                .WithMany(membership => membership.Payments)
                .HasForeignKey(payment => payment.Membership_id);

            modelBuilder.Entity<Classes>()
                .HasOne(classes => classes.User)
                .WithMany(user => user.CoachedClasses)
                .HasForeignKey(classes => classes.Coach_id);

            modelBuilder.Entity<Booking>()
                .HasOne(booking => booking.User)
                .WithMany(user => user.Bookings)
                .HasForeignKey(booking => booking.User_id);

            modelBuilder.Entity<Booking>()
                .HasOne(booking => booking.ClassBooked)
                .WithMany(classes => classes.Bookings)
                .HasForeignKey(booking => booking.Class_id);
        }
    }  
}
