using APIMMA.Data;
using APIMMA.Dtos;
using APIMMA.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace APIMMA.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserDto> getUserById(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == userId);

            if (user == null)
            {
                throw new UserNotFoundException(userId);
            }

            return new UserDto
            {
                name = user.Name,
                nickname = user.Nickname != null ? user.Nickname : "N/A",
                email = user.Email,
                role = user.Role
            };
        }
    }
}
