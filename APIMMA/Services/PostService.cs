using APIMMA.Data;
using APIMMA.Dtos.PostDtos;
using APIMMA.Dtos.UserDtos;
using APIMMA.Exceptions;
using APIMMA.Models;
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
                        nickname = post.User.Nickname ?? "N/A",
                        role = post.User.Role,
                    }
                }).ToListAsync();

            return posts;
        }

        public async Task Post(int UserId, CreatePostDto postDto)
        {
            var user = await _context.Users.FindAsync(UserId);

            if (user == null)
            {
                throw new UserNotFoundException(UserId);
            }

            var post = new Post
            {
                Title = postDto.Title,
                Content = postDto.Content,
                Created_at = DateTime.UtcNow,
                User_id = user.Id
            };

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
        }

        public async Task EditPost(int postId, int userId, PatchPostDto postDto)
        {
            var post = await _context.Posts.FindAsync(postId);
            if (post == null)
            {
                throw new PostNotFoundException(postId);
            }
            if (post.User_id != userId)
            {
                throw new UnauthorizedAccessException("You are not authorized to edit this post.");
            }

            post.Title = postDto.Title ?? post.Title;
            post.Content = postDto.Content ?? post.Content;

            await _context.SaveChangesAsync();
        }


    }
}
