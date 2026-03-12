using APIMMA.Dtos.PostDtos;

namespace APIMMA.Services
{
    public interface IPostService
    {
        public Task<List<PostDto>> GetPosts(int page, int pageSize);

        public Task<List<UserPostDto>> GetPostsByUser(int userId, int page, int pageSize);

        public Task Post(int UserId, CreatePostDto postDto);

        public Task EditPost(int postId, int userId, PatchPostDto postDto);
    }
}
