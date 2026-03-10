using APIMMA.Dtos;

namespace APIMMA.Services
{
    public interface IUserService
    {
        public Task<UserDto> getUserById(int userId);
    }
}
