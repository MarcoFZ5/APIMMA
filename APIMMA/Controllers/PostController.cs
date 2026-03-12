using APIMMA.Dtos.PostDtos;
using APIMMA.Extensions;
using APIMMA.Services;
using FluentValidation;
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
        private readonly IValidator<CreatePostDto> _createPostValidator;

        public PostController(IPostService postService, IValidator<CreatePostDto> createPostValidator)
        {
            _postService = postService;
            _createPostValidator = createPostValidator;
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
            await _createPostValidator.ValidateAndThrowAsync(postDto);

            int userId = User.GetUserId();

            await _postService.Post(userId, postDto);

            return Created();
        }

        [HttpPatch("{postId}")]
        public async Task<ActionResult> EditPost(int postId, [FromBody] PatchPostDto postDto)
        {
            int userId = User.GetUserId();

            await _postService.EditPost(userId, postId, postDto);

            return NoContent();
        }
    }

}
