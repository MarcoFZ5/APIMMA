using APIMMA.Dtos.UserDtos;

namespace APIMMA.Services
{
    public interface IUserService
    {
        public Task<UserDto> getUserById(int userId);

        public Task updateProfile(int userId, UpdateProfileDto userUpdateDto);
    }
}
