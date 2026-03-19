using APIMMA.Data;
using APIMMA.Dtos.UserDtos;
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

        public async Task<UserDto> GetUserById(Guid userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Id.Equals(userId));

            if (user == null)
            {
                throw new UserNotFoundException(userId);
            }

            return new UserDto
            {
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt,
                Weight = user.Weight ?? 0,
                Discipline = user.Discipline ?? "N/A",
                Level = user.Level ?? "N/A",
                Gym = user.Gym ?? "N/A"
            };
        }

        public async Task UpdateProfile(Guid userId, UpdateProfileDto userUpdateDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Id.Equals(userId));

            user.Username = userUpdateDto.Username ?? user.Username;

            user.Weight = userUpdateDto.Weight ?? user.Weight;

            user.Discipline = userUpdateDto.Discipline ?? user.Discipline;

            user.Level = userUpdateDto.Level ?? user.Level;

            user.Gym = userUpdateDto.Gym ?? user.Gym;

            await _context.SaveChangesAsync();
        }
    }
}
