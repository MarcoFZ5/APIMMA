using APIMMA.Dtos.LikeDtos;
using APIMMA.Dtos.UserDtos;

namespace APIMMA.Services
{
    public interface ILikeService
    {
        public Task<ResponseLike> LikePost (Guid postId, Guid userId);

        public Task<ICollection<UserSimplifiedDto>> GetUsersWhoLiked (Guid postId, int page, int pageSize);
    }
}
