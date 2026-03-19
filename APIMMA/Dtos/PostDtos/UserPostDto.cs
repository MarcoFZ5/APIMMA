using System.Numerics;

namespace APIMMA.Dtos.PostDtos
{
    public class UserPostDto
    {
        public int Id { get; set; }
        public string Content { get; set; }

        public string? Type { get; set; } // MANUAL | TRAININGLOG | CHALLENGE

        public BigInteger LikesCount { get; set; }
        public DateTime Created_at { get; set; }
    }
}
