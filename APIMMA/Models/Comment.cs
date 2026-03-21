using Microsoft.EntityFrameworkCore.Metadata;

namespace APIMMA.Models
{
    public class Comment
    {
        public Guid Id { get; set; }

        public string Content { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int? IsDeleted { get; set; } = 0;

        public DateTime? DeletedAt { get; set; }

        // Navigation properties
        // One comment belongs to one post
        public Guid PostId { get; set; }
        public Post Post { get; set; }

        // One comment belongs to one user
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
