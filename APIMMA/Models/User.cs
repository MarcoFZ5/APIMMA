namespace APIMMA.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }

        // NULLABLE PROPERTIES
        public float? Weight { get; set; }

        public string? Discipline { get; set; }

        public string? Level { get; set; }

        public string? Gym { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        // Navigation properties
        
    }
}
