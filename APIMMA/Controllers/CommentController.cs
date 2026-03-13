using APIMMA.Dtos.CommentDtos;
using APIMMA.Extensions;
using APIMMA.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIMMA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

    }
}
