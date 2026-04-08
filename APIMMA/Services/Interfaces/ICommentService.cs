using APIMMA.Dtos.CommentDtos;

namespace APIMMA.Services.Interfaces
{
    public interface ICommentService
    {
        public Task<CommentDto> AddComment(Guid postId, Guid userId, CommentPostDto commentDto);
        public Task EditComment();
        public Task DeleteComment(Guid userId, Guid commentId);
    }
}
