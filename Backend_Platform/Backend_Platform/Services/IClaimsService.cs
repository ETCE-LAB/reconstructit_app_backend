using System.Security.Claims;

namespace WebApplication1.Services
{
    public interface IClaimsService
    {
        public string? GetUserAccountId(ClaimsPrincipal user);
    }
}
