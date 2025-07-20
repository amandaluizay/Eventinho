using Eventinho.Shared.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Eventinho.Shared.Services
{
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetCurrentUserEmail()
        {
            string? Email = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;
            return string.IsNullOrEmpty(Email) ? "system without user" : Email;
        }

        public string GetCurrentUserName()
        {
            string? Name = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;
            return string.IsNullOrEmpty(Name) ? "system without user" : Name;
        }

    }
}
