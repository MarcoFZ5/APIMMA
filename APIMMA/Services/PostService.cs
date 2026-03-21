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

        // FEED
        public async Task<List<PostDto>> GetPostsFeed(Guid currentUserId, int page, int pageSize)
        {
            var posts = await _context.Posts
                .AsNoTracking()
                .Where(p =>
                    p.UserId == currentUserId ||
                        _context.Follows.Any(f =>
                             f.FollowerId == currentUserId && f.OwnerId == p.UserId)) // GET POSTS IF IM THE OWNER OR IF IM FOLLOWING THE OWNER
                .OrderByDescending(post => post.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(post => new PostDto
                {
                    Id = post.Id,
                    Content = post.Content,
                    Created_at = post.CreatedAt,
                    User = new UserSimplifiedDto
                    {
                        Id = post.UserId,
                        Username = post.userOwner.Username
                    }
                }
                ).ToListAsync();

            return posts;
        }

        // GET POSTS WITH PAGINATION
        public async Task<List<PostDto>> GetPosts(int page, int pageSize)
        {
            var posts = await _context.Posts
                .AsNoTracking()
                .OrderByDescending(post => post.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(post => new PostDto
                {
                    Id = post.Id,
                    Content = post.Content,
                    Type = post.Type,
                    Created_at = post.CreatedAt,
                    LikesCount = post.LikesCount,
                    User = new UserSimplifiedDto
                    {
                        Id = post.UserId,
                        Username = post.userOwner.Username
                    }
                }).ToListAsync();

            return posts;
        }

        // GET POSTS BY USER WITH PAGINATION
        public async Task<List<UserPostDto>> GetPostsByUser(Guid userId, int page, int pageSize)
        {
            var userExists = await _context.Users.AnyAsync(user => user.Id.Equals(userId));

            if (!userExists)
            {
                throw new UserNotFoundException(userId);
            }

            var posts = await _context.Posts
                .AsNoTracking()
                .Where(post => post.UserId.Equals(userId))
                .OrderByDescending(post => post.CreatedAt)
                .Skip((page -1) * pageSize)
                .Take(pageSize)
                .Select(post => new UserPostDto
                {
                    Id = post.Id,
                    Content = post.Content,
                    Type = post.Type,
                    LikesCount = post.LikesCount,
                    Created_at = post.CreatedAt,
                }).ToListAsync();

            return posts;
        }


        // GET POST BY ID
        public async Task<PostDto> GetPostById(Guid postId)
        {
            var post = await _context.Posts
                .AsNoTracking()
                .Where(p => p.Id.Equals(postId))
                .Select(p => new PostDto
                {
                    Id = p.Id,
                    Content = p.Content,
                    Created_at = p.CreatedAt,
                    Type = p.Type,
                    LikesCount = p.LikesCount,
                    User = new UserSimplifiedDto
                    {
                        Id = p.UserId,
                        Username = p.userOwner.Username
                    }
                }).FirstOrDefaultAsync() 
                ?? throw new PostNotFoundException(postId);

            return post;
        }

        public async Task<List<CommentDto>> GetCommentsByPostId(Guid postId, int page, int pageSize)
        {

            var exists = await _context.Posts.AnyAsync(post => post.Id.Equals(postId));
                
            if (!exists)
            {
               throw new PostNotFoundException(postId);
            }

            var comments = await _context.Comments
                .AsNoTracking()
                .Where(comment => comment.PostId.Equals(postId))
                .OrderByDescending(comment => comment.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(comment => new CommentDto
                {
                    Id = comment.Id,
                    Content = comment.Content,
                    CreatedAt = comment.CreatedAt,
                    User = new UserSimplifiedDto
                    {
                        Id = comment.UserId,
                        Username = comment.User.Username
                    }   
                }).ToListAsync();

            return comments;
        }

        public async Task CreatePost(string Type, Guid UserId, CreatePostDto postDto)
        {
            var user = await _context.Users.FindAsync(UserId);

            if (user == null)
            {
                throw new UserNotFoundException(UserId);
            }

            var post = new Post
            {
                Content = postDto.Content,
                CreatedAt = DateTime.UtcNow,
                Type = Type,
                UserId = UserId,
            };

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
        }

        public async Task EditPost(Guid postId, Guid userId, PatchPostDto postDto)
        {
            System.Console.WriteLine($"Editing post with ID: {postId} by user: {userId}");
            System.Console.WriteLine($"Type of the postId: {postId.GetType()}");

            var post = await _context.Posts.FindAsync(postId);
            if (post == null)
            {
                throw new PostNotFoundException(postId);
            }
            if (post.UserId != userId)
            {
                throw new UnauthorizedAccessException("You are not authorized to edit this post.");
            }

            post.Content = postDto.Content ?? post.Content;

            await _context.SaveChangesAsync();
        }

    }
}
