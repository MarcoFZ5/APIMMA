namespace APIMMA.Models
{
    public class Like
    {
        // Navigation properties
        public Guid PostId { get; set; }
        public Post Post { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;        
    }
}
