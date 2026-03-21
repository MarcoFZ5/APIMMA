using System.Numerics;

namespace APIMMA.Models
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Type { get; set; } // MANUAL | TRAININGLOG | CHALLENGE
        public long LikesCount { get; set; } = 0;

        public Guid? ReferenceId { get; set; } // For referencing a training log or challenge if the post is of type TRAININGLOG or CHALLENGE ELSE NULL


        // Navigation properties

        // One post have one user (owner)
        public Guid UserId { get; set; }
        public User userOwner { get; set; }

        // One post can have many comments
        public List<Comment> Comments { get; set; }

        // One post can have many likes
        public List<Like> Likes { get; set; }

    }
  
}
