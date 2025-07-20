using Eventinho.Domain.Entities;
using System.Security.Claims;

namespace TesteJWT.Interfaces
{
    public interface ITesteUserService
    {
        string GenerateToken(User user);
        ClaimsIdentity GenerateClaims(User user);
        string GetCurrentUserEmail();
        string GetCurrentUserName();
        Task<string> CreateUserAsync(string userName, string email, string password);
        Task<string> LoginUserAsync(string email, string password);
    }
}
