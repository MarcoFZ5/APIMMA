using APIMMA.Dtos.PostDtos;

namespace APIMMA.Services
{
    public interface IPostService
    {
        public Task<List<PostDto>> GetPosts(int page, int pageSize);
    }
}
