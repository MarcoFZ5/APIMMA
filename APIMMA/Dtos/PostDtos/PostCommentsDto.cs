using APIMMA.Dtos.CommentDtos;

namespace APIMMA.Dtos.PostDtos
{
    public class PostCommentsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public DateTime Created_at { get; set; }

        public List<CommentDto>? Comments { get; set; }
    }
}
