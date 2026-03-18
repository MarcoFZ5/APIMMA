using Microsoft.EntityFrameworkCore.Metadata;

namespace APIMMA.Models
{
    public class Comment
    {
        public Guid Id { get; set; }

        public string Content { get; set; }

        public DateTime Created_at { get; set; } = DateTime.UtcNow;

        public int? IsDeleted { get; set; } = 0;

        public DateTime? Deleted_at { get; set; }

        // Navigation properties
        // One comment belongs to one post
        public int Post_id { get; set; }
        public Post Post { get; set; }

        // One comment belongs to one user
        public int User_id { get; set; }
        public User User { get; set; }
    }
}
