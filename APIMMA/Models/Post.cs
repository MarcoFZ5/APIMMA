using System.Numerics;

namespace APIMMA.Models
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime Created_at { get; set; } = DateTime.UtcNow;
        public string Type { get; set; } // MANUAL | TRAININGLOG | CHALLENGE
        public BigInteger LikesCount { get; set; } = 0;

        public Guid? ReferenceId { get; set; } // For referencing a training log or challenge if the post is of type TRAININGLOG or CHALLENGE ELSE NULL
        // Navigation properties

    }
  
}
