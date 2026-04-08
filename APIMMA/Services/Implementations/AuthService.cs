using APIMMA.BackgroundJobs.Emails;
using APIMMA.Data;
using APIMMA.Dtos.AuthDtos;
using APIMMA.Dtos.UserDtos;
using APIMMA.Exceptions;
using APIMMA.Models;
using APIMMA.Services.Interfaces;
using FluentValidation;
using Hangfire;
using Mapster;
using Mapster.Utils;
using Microsoft.EntityFrameworkCore;

namespace APIMMA.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IJwtService _jwtService;
        private readonly IEmailJobs _jobs;

        public AuthService(AppDbContext context, IValidator<RegisterUserDto> validator, IJwtService jwtService, IEmailJobs jobs)
        {
            _context = context;
            _jwtService = jwtService;
            _jobs = jobs;
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
            var emailExists = await _context.Users.AnyAsync(user => user.Email == userDto.Email);
            var usernameExists = await _context.Users.AnyAsync(user => user.Username == userDto.Username);
    
            if (emailExists) throw new EmailAlreadyExistsException(userDto.Email);
            
            if (usernameExists) throw new UsernameAlreadyExistsException(userDto.Username);

            var newUser = new User
            {
                Username = userDto.Username,
                Email = userDto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
                Role = RoleEnum.USER.ToString()
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            var response = newUser.Adapt<UserDto>();

            // inject the background job to send a confirmation email after registration
            BackgroundJob.Enqueue<IEmailJobs>(jobs => jobs.sendConfirmationEmail(response.Email));

            return response;
        }

        public async Task<UserDto> Me (Guid UserId)
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
