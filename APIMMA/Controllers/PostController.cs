using APIMMA.Dtos.PostDtos;
using APIMMA.Extensions;
using APIMMA.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace APIMMA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public async Task<ActionResult<PostDto>> GetPosts(int page, int pageSize)
        {
            var posts = await _postService.GetPosts(page, pageSize);

            return Ok(posts);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreatePostDto postDto)
        {
            int userId = User.GetUserId();

            await _postService.Post(userId, postDto);

            return Created();
        }
    }
}
