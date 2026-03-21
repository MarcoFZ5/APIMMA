using System.Numerics;

namespace APIMMA.Dtos.PostDtos
{
    public class UserPostDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; }

        public string? Type { get; set; } // MANUAL | TRAININGLOG | CHALLENGE

        public long LikesCount { get; set; }
        public DateTime Created_at { get; set; }
    }
}
