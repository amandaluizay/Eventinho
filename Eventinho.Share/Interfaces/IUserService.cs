using Eventinho.Domain.Entities;

namespace Eventinho.Shared.Interfaces
{
    public interface IUserService
    {

        Task<string> CreateUserAsync(string userName, string email, string password);
        Task<string> LoginUserAsync(string email, string password);
        Task<User> GetUserByEmailAsync(string email);
    }
}