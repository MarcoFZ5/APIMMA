using APIMMA.Dtos.AuthDtos;
using APIMMA.Dtos.UserDtos;

namespace APIMMA.Services
{
    public interface IAuthService
    {

        Task<JwtDto> Login(LoginUserDto userDto);
        Task<String> Register(RegisterUserDto userDto);

        Task<UserDto> Me(int UserId);
    }
}
