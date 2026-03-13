using APIMMA.Data;
using APIMMA.Dtos.CommentDtos;
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
                    User = new UserDto
                    {
                        name = post.User.Name,
                        nickname = post.User.Nickname ?? "N/A",
                        role = post.User.Role,
                    }
                }).ToListAsync();

            return posts;
        }

        public async Task<List<UserPostDto>> GetPostsByUser(int userId, int page, int pageSize)
        {
            var userExists = await _context.Users.AnyAsync(user => user.Id == userId);

            if (!userExists)
            {
                throw new UserNotFoundException(userId);
            }

            var posts = await _context.Posts
                .AsNoTracking()
                .Where(post => post.User_id == userId)
                .OrderByDescending(post => post.Created_at)
                .Skip((page -1) * pageSize)
                .Take(pageSize)
                .Select(post => new UserPostDto
                {
                    Id = post.Id,
                    Title = post.Title,
                    Content = post.Content,
                    Created_at = post.Created_at
                }).ToListAsync();

            return posts;
        }

        public async Task<PostDto> GetPostById(int postId)
        {
            var post = await _context.Posts
                .AsNoTracking()
                .Where(p => p.Id == postId)
                .Select(p => new PostDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    Content = p.Content,
                    Created_at = p.Created_at,
                    User = new UserDto
                    {
                        name = p.User.Name,
                        nickname = p.User.Nickname ?? "N/A",
                        role = p.User.Role,
                    }
                }).FirstOrDefaultAsync() 
                ?? throw new PostNotFoundException(postId);

            return post;
        }

        public async Task<List<CommentDto>> GetCommentsByPostId(int postId, int page, int pageSize)
        {

            var exists = await _context.Posts.AnyAsync(post => post.Id == postId);
                
            if (!exists)
            {
               throw new PostNotFoundException(postId);
            }

            var comments = await _context.Comments
                .AsNoTracking()
                .Where(comment => comment.Post_id == postId)
                .OrderByDescending(comment => comment.Created_at)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(comment => new CommentDto
                {
                    Id = comment.Id,
                    Content = comment.Content,
                    Created_at = comment.Created_at,
                    User = new UserDto
                    {
                        name = comment.User.Name,
                        nickname = comment.User.Nickname ?? "N/A",
                        role = comment.User.Role,
                    }
                }).ToListAsync();

            return comments;
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
