namespace APIMMA.Models
{
    public class Like
    {
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
    }
}
