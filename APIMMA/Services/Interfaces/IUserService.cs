using APIMMA.Dtos.UserDtos;

namespace APIMMA.Services.Interfaces
{
    public interface IUserService
    {
        public Task<UserDto> GetUserById(Guid userId);

        public Task UpdateProfile(Guid userId, UpdateProfileDto userUpdateDto);
    }
}
