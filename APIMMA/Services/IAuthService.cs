using APIMMA.Dtos;

namespace APIMMA.Services
{
    public interface IAuthService
    {

        Task<JwtDto> Login(LoginUserDto userDto);
        Task<String> Register(RegisterUserDto userDto);

        Task<UserDto> Me(int UserId);
    }
}
