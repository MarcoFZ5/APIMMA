using APIMMA.Data;
using APIMMA.Dtos;
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

            if (!BCrypt.Net.BCrypt.Verify(userDto.Password, user.Password))
            {
                throw new ConflictException("Invalid email or password");
            }

            var token = _jwtService.GenerateToken(user);
            return new JwtDto { Token = token };
        }

        public async Task<string> Register(RegisterUserDto userDto)
        {
            var user = _context.Users.AnyAsync(user => user.Email == userDto.Email).Result;

            if (user)
            {
                throw new EmailAlreadyExistsException(userDto.Email);
            }

            var newUser = new User
            {
                Name = userDto.Name,
                Email = userDto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
                Role = RoleEnum.USER.ToString()
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return "User registered successfully";
        }

        public async Task<UserDto> Me (int UserId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == UserId);

            if (user == null)
            {
                throw new UserNotFoundException(UserId);
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
