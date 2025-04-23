using Eventinho.Domain.Entities;

namespace Eventinho.Shared.Interfaces
{
    public interface ITokenService
    {
        string Generate(User user);
    }
}