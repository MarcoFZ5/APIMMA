using APIMMA.Dtos.LikeDtos;
using APIMMA.Extensions;
using APIMMA.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIMMA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikesController : ControllerBase
    {
        private readonly ILikeService _likeService;

        public LikesController(ILikeService likeService)
        {
            _likeService = likeService;
        }

        [HttpPost("{postId}")]
        public async Task<ActionResult<ResponseLike>> LikePost(Guid postId)
        {
            var userId = User.GetUserId();
            var response = await _likeService.LikePost(postId, userId);
            return Ok(response);
        }
    }
}
