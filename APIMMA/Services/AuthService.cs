using APIMMA.Data;
using APIMMA.Dtos.AuthDtos;
using APIMMA.Dtos.UserDtos;
using APIMMA.Exceptions;
using APIMMA.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace APIMMA.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IJwtService _jwtService;

        public AuthService(AppDbContext context, IValidator<RegisterUserDto> validator, IJwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        public async Task<JwtDto> Login (LoginUserDto userDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Email == userDto.Email);

            if (user == null)
            {
                throw new ConflictException("Invalid email or password");
            }

            if (!BCrypt.Net.BCrypt.Verify(userDto.Password, user.PasswordHash))
            {
                throw new ConflictException("Invalid email or password");
            }

            var token = _jwtService.GenerateToken(user);
            return new JwtDto { Token = token };
        }

        public async Task<UserDto> Register(RegisterUserDto userDto)
        {
            var user = _context.Users.AnyAsync(user => user.Email == userDto.Email).Result;

            if (user)
            {
                throw new EmailAlreadyExistsException(userDto.Email);
            }

            var newUser = new User
            {
                Username = userDto.Username,
                Email = userDto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
                Role = RoleEnum.USER.ToString()
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            var response = new UserDto
            {
                Username = newUser.Username,
                Email = newUser.Email,
                Role = newUser.Role,
                CreatedAt = newUser.CreatedAt,
                Weight = newUser.Weight ?? 0,
                Discipline = newUser.Discipline ?? "N/A",
                Level = newUser.Level ?? "N/A",
                Gym = newUser.Gym ?? "N/A"
            };

            return response;
        }

        public async Task<UserDto> Me (int UserId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Id.Equals(UserId));

            if (user == null)
            {
                throw new UserNotFoundException(UserId);
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

    }
}
