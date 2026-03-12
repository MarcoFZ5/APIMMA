using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace APIMMA.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetUserId(this ClaimsPrincipal user)
        {
            var claim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (claim == null)
            {
                throw new UnauthorizedAccessException("User ID claim not found.");
            }

            return int.Parse(claim.Value);
        } 
    }
}
