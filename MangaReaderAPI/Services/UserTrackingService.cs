using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;

namespace MangaReaderAPI.Services
{
    public class UserTrackingService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserTrackingService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Every request has the User property HttpContext.User where user is a type of ClaimsPrincipal.
        /// ClaimsPrincipal represents the authenticated user and contains our JWT Claims:
        /// sub which is the user ID
        /// email and role
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int? GetUserId()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            // Check if Token is expired and if it validated by us.
            if (user?.Identity?.IsAuthenticated != true) return null;
            var claim = user.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null && int.TryParse(claim.Value, out var userId)) { return userId; }
            return null;
        }
    }
}