using APIMMA.Dtos.CommentDtos;
using APIMMA.Dtos.PostDtos;

namespace APIMMA.Services
{
    public interface IPostService
    {
        public Task CreatePost(int UserId, CreatePostDto postDto);
        public Task<List<PostDto>> GetPosts(int page, int pageSize);

        public Task<List<PostDto>> GetPostsFeed(int page, int pageSize);

        public Task<PostDto> GetPostById(int postId);

        public Task<List<UserPostDto>> GetPostsByUser(int userId, int page, int pageSize);

        public Task EditPost(int postId, int userId, PatchPostDto postDto);
        public Task<List<CommentDto>> GetCommentsByPostId(int postId, int page, int pageSize);
    }
}
