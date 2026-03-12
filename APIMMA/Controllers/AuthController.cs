using APIMMA.Dtos;
using APIMMA.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace APIMMA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IValidator<LoginUserDto> _loginValidator;
        private readonly IValidator<RegisterUserDto> _registerValidator;

        public AuthController(IAuthService authService, IValidator<LoginUserDto> loginValidator, IValidator<RegisterUserDto> registerValidator)
        {
            _authService = authService;
            _loginValidator = loginValidator;
            _registerValidator = registerValidator;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<JwtDto>> Login([FromBody] LoginUserDto userDto)
        {
            await _loginValidator.ValidateAndThrowAsync(userDto);
            return await _authService.Login(userDto);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<String>> Register([FromBody] RegisterUserDto userDto)
        {
            await _registerValidator.ValidateAndThrowAsync(userDto);
            return await _authService.Register(userDto);
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<ActionResult<UserDto>> Me()
        {
            var claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);


            var UserId = int.Parse(claim.Value);

            return await _authService.Me(UserId);
        }
    }
}
