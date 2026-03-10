using APIMMA.Models;

namespace APIMMA.Services
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
