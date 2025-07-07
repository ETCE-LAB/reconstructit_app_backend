using System.Security.Claims;

namespace WebApplication1.Services.Implementation
{
    public class ClaimsService : IClaimsService
    {
        public string? GetUserAccountId(ClaimsPrincipal user)
        {
            var claimValue = user.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
            if (claimValue == null)
            {
                return null;
            }
            return claimValue;
        }
    }
}
