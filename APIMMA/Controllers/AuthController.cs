using APIMMA.Dtos;
using APIMMA.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace APIMMA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<JwtDto>> Login([FromBody] LoginUserDto userDto)
        {
            return await _authService.Login(userDto);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<String>> Register([FromBody] RegisterUserDto userDto)
        {
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
