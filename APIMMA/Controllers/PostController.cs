using APIMMA.Dtos.CommentDtos;
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
    //[Authorize]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;
        private readonly IValidator<CreatePostDto> _createPostValidator;

        public PostController(IPostService postService, IValidator<CreatePostDto> createPostValidator, ICommentService commentService)
        {
            _postService = postService;
            _commentService = commentService;
            _createPostValidator = createPostValidator;
        }

        [HttpGet]
        public async Task<ActionResult<PostDto>> GetPosts(int page, int pageSize)
        {
            var posts = await _postService.GetPosts(page, pageSize);

            return Ok(posts);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<UserPostDto>> GetPostsByUser(Guid userId, int page, int pageSize)
        {
            var posts = await _postService.GetPostsByUser(userId, page, pageSize);
            return Ok(posts);
        }

        [HttpGet("{postId}")]
        public async Task<ActionResult<PostDto>> GetPostById(Guid postId)
        {
            var post = await _postService.GetPostById(postId);
            return Ok(post);
        }

        [HttpGet("{postId}/comments")]
        public async Task<ActionResult<List<CommentDto>>> GetCommentsForPost(Guid postId, int page, int pageSize)
        {
            var comments = await _postService.GetCommentsByPostId(postId, page, pageSize);
            return Ok(comments);
        }

        [HttpPost]
        public async Task<ActionResult> CreatePost([FromBody] CreatePostDto postDto)
        {
            await _createPostValidator.ValidateAndThrowAsync(postDto);

            Guid userId = User.GetUserId();

            await _postService.CreatePost("Manual", userId, postDto);

            return Created();
        }

        [HttpPost("{postId}/comments")]
        public async Task<ActionResult<CommentDto>> AddComment(Guid postId, [FromBody] CommentPostDto commentDto)
        {
            Guid userId = User.GetUserId();
            var createdComment = await _commentService.AddComment(postId, userId, commentDto);

            return CreatedAtAction(nameof(AddComment), new { id = createdComment.Id }, createdComment);
        }

        [HttpPatch("{postId}")]
        public async Task<ActionResult> EditPost(Guid postId, [FromBody] PatchPostDto postDto)
        {
            Guid userId = User.GetUserId();

            await _postService.EditPost(postId, userId, postDto);

            return NoContent();
        }
    }

}
