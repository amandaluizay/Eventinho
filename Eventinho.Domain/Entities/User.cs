namespace Eventinho.Domain.Entities
{
    public class User : Entity
    {
        public string? UserName { get; set; }

        public string? Email { get; set; }

        public string? PasswordHash { get; set; }

        public List<UserNotification>? Notifications { get; set; }
        public List<Event>? Events { get; set; }
        public List<EventColaborator>? EventColaborators { get; set; }
        public List<Guest>? EventGuests { get; set; }
        public List<UserInterationRequest>? SenderRequests { get; set; }
        public List<UserInterationRequest>? ReceiverRequests { get; set; }
    }
}