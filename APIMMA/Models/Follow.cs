namespace APIMMA.Models
{
    public class Follow
    {
        // Navigation Properties to be added
        public Guid OwnerId { get; set; }
        public User Owner { get; set; }

        public Guid FollowerId { get; set; }
        public User Follower { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
