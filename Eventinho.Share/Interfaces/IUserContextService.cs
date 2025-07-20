namespace Eventinho.Shared.Interfaces
{
    public interface IUserContextService
    {
        string GetCurrentUserEmail();
        string GetCurrentUserName();
    }
}
