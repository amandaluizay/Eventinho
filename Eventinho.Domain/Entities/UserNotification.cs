namespace Eventinho.Domain.Entities
{
    public class UserNotification : Entity
    {
        public User User { get; set; }
        public Guid UserId { get; set; }

        public string Message { get; set; }
        public bool IsRead { get; set; }
    }
}
