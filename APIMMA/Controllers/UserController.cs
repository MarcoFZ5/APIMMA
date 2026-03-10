using APIMMA.Dtos;
using APIMMA.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIMMA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController (IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("/{userId}")]
        public async Task<ActionResult<UserDto>> getUserById(int userId)
        {
            var user = await _userService.getUserById(userId);
            return Ok(user);
        }

    }
}
