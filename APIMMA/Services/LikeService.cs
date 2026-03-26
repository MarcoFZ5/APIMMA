
using APIMMA.Data;
using APIMMA.Dtos.LikeDtos;
using APIMMA.Dtos.UserDtos;
using APIMMA.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace APIMMA.Services
{
    public class LikeService : ILikeService
    {
        public readonly AppDbContext _context;
        public LikeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<UserSimplifiedDto>> GetUsersWhoLiked(Guid postId, int page, int pageSize)
        {
            var users = await _context.Likes.Where(l => l.PostId == postId)
                .AsNoTracking()
                .OrderByDescending(l => l.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(l => new UserSimplifiedDto
                {
                    Id = l.User.Id,
                    Username = l.User.Username
                }).ToListAsync();

            return users;
        }

        public async Task<ResponseLike> LikePost(Guid postId, Guid userId)
        {
            var post = await _context.Posts.FindAsync(postId);

            if (post == null) throw new NotFoundException("Post not found");

            var like = await _context.Likes.FirstOrDefaultAsync(l => l.PostId == postId && l.UserId == userId);

            if (like != null) // we remove the like if it already exists (toggle behavior)
            {
                _context.Likes.Remove(like);
                await _context.Posts
                    .Where(p => p.Id == postId)
                    .ExecuteUpdateAsync(s => s.SetProperty(p => p.LikesCount, p => p.LikesCount - 1));
            }
            else
            {
                _context.Likes.Add(new Models.Like
                {
                    PostId = postId,
                    UserId = userId,
                    CreatedAt = DateTime.UtcNow
                });

                await _context.Posts
                    .Where(p => p.Id == postId)
                    .ExecuteUpdateAsync(s => s.SetProperty(p => p.LikesCount, p => p.LikesCount + 1));
            }

            await _context.SaveChangesAsync();

            return new ResponseLike
            {
                Liked = like == null, // if like is null it means we just added a like, otherwise we removed it
                Count = like != null ? post.LikesCount - 1 : post.LikesCount + 1
            };

        }

    }
}
