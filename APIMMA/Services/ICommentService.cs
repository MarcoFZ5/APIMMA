using APIMMA.Dtos.CommentDtos;

namespace APIMMA.Services
{
    public interface ICommentService
    {
        public Task<CommentDto> AddComment(Guid postId, Guid userId, CommentPostDto commentDto);
        public Task EditComment();
        public Task DeleteComment();
    }
}
