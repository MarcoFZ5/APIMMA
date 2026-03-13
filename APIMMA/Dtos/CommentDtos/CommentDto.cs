using APIMMA.Dtos.UserDtos;

namespace APIMMA.Dtos.CommentDtos
{
    public class CommentDto
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime Created_at { get; set; }

        public UserDto User { get; set; }
    }
}
