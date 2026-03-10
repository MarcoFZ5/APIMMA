namespace APIMMA.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Nickname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        // Navigation properties
        // One user can have many posts
        public ICollection<Post> Posts { get; set; }

        // One user can have many comments
        public ICollection<Comment> Comments { get; set; }

        // One user can have many memberships
        public ICollection<Membership> Memberships { get; set; }

        // One user can have many bookings
        public ICollection<Booking> Bookings { get; set; }

        // One user can have many classes as a coach
        public ICollection<Classes> CoachedClasses { get; set; }
    }
}
