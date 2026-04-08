using APIMMA.Dtos.AuthDtos;
using APIMMA.Dtos.UserDtos;

namespace APIMMA.Services.Interfaces
{
    public interface IAuthService
    {

        Task<JwtDto> Login(LoginUserDto userDto);
        Task<UserDto> Register(RegisterUserDto userDto);
        Task<UserDto> Me(Guid UserId);
    }
}
