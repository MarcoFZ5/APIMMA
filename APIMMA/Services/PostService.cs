using APIMMA.Data;
using APIMMA.Dtos.PostDtos;
using APIMMA.Dtos.UserDtos;
using Microsoft.EntityFrameworkCore;

namespace APIMMA.Services
{
    public class PostService : IPostService
    {
        private readonly AppDbContext _context;

        public PostService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<PostDto>> GetPosts(int page, int pageSize)
        {
            var posts = await _context.Posts
                .AsNoTracking()
                .OrderByDescending(post => post.Created_at)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(post => new PostDto
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                Created_at = post.Created_at,
                user = new UserDto
                {
                    name = post.User.Name,
                    nickname = post.User.Nickname ?? "N/A"
                }
            }).ToListAsync();

            return posts;
        }

    }
}
