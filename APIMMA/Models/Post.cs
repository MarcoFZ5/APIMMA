namespace APIMMA.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Created_at { get; set; } = DateTime.UtcNow;

        // Navigation properties

        // One post has manny comments
        public ICollection<Comment> Comments { get; set; }

        // One post belongs to one user
        public int User_id { get; set; }
        public User User { get; set; }
    }
}
