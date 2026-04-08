using APIMMA.Models;

namespace APIMMA.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
