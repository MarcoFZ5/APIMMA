using APIMMA.Dtos.CommentDtos;
using APIMMA.Dtos.PostDtos;

namespace APIMMA.Services
{
    public interface IPostService
    {
        //
        public Task CreatePost(Guid UserId, CreatePostDto postDto);
        //
        public Task<List<PostDto>> GetPosts(int page, int pageSize);

        public Task<List<PostDto>> GetPostsFeed(Guid currentUserId, int page, int pageSize);
        //
        public Task<PostDto> GetPostById(Guid postId);

        //
        public Task<List<UserPostDto>> GetPostsByUser(Guid userId, int page, int pageSize);
        //
        public Task EditPost(Guid postId, Guid userId, PatchPostDto postDto);
        // 
        public Task<List<CommentDto>> GetCommentsByPostId(Guid postId, int page, int pageSize);
    }
}
