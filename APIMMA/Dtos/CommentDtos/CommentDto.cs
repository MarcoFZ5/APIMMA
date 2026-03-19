using APIMMA.Dtos.UserDtos;

namespace APIMMA.Dtos.CommentDtos
{
    public class CommentDto
    {
        public Guid Id { get; set; }

        public string Content { get; set; }

        public DateTime Created_at { get; set; }

        public UserSimplifiedDto User { get; set; }
    }
}
