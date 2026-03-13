using APIMMA.Dtos.CommentDtos;

namespace APIMMA.Services
{
    public interface ICommentService
    {
        public Task<CommentDto> AddComment(int postId, int userId, CommentPostDto commentDto);
        public Task EditComment();
        public Task DeleteComment();
    }
}
