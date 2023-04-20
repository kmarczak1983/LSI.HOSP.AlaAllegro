using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace LSI.HOSP.AlaAllegro.Infrastructure.Services
{
    public interface ICurrentUserService
    {
        ClaimsPrincipal User { get; }
        int? GetUserId { get; }
    }

    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ClaimsPrincipal User => _httpContextAccessor.HttpContext?.User;

        public int? GetUserId => User is null || User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier) is null ? null : int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
    }
}
