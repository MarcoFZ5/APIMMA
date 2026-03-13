
using APIMMA.Data;
using APIMMA.Dtos.CommentDtos;
using APIMMA.Dtos.UserDtos;
using APIMMA.Exceptions;
using APIMMA.Models;
using Microsoft.EntityFrameworkCore;

namespace APIMMA.Services
{
    public class CommentService : ICommentService
    {
        private readonly AppDbContext _context;
        public CommentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CommentDto> AddComment(int postId, int userId, CommentPostDto commentDto)
        {
            var comment = new Comment
            {
                Content = commentDto.Content,
                Post_id = postId,
                User_id = userId,
                Created_at = DateTime.UtcNow
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            var user = await _context.Users.FindAsync(userId);

            return new CommentDto
            {
                Id = comment.Id,
                Content = comment.Content,
                Created_at = comment.Created_at,
                User = new UserDto
                {
                    name = user.Name,
                    nickname = user.Nickname,
                    email = user.Email,
                    role = user.Role
                }
            };
        }

        public Task DeleteComment()
        {
            throw new NotImplementedException();
        }

        public Task EditComment()
        {
            throw new NotImplementedException();
        }
    }
}
