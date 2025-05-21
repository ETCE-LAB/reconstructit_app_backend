using System.Security.Claims;

namespace WebApplication1.Services.Implementation
{
    public class ClaimsService : IClaimsService
    {
        public string? GetUserAccountId(ClaimsPrincipal user)
        {
            var claimValue = user.FindFirstValue("id");
            if (claimValue == null)
            {
                return null;
            }

            return claimValue;
        }
    }
}
