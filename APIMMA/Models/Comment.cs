namespace APIMMA.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime Created_at { get; set; } = DateTime.UtcNow;

        // Navigation properties
        // One comment belongs to one post
        public int Post_id { get; set; }
        public Post Post { get; set; }

        // One comment belongs to one user
        public int User_id { get; set; }
        public User User { get; set; }
    }
}
