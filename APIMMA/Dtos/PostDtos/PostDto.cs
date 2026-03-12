using APIMMA.Dtos.UserDtos;

namespace APIMMA.Dtos.PostDtos
{
    public class PostDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime Created_at { get; set; }

        public UserDto user { get; set; }

    }
}
