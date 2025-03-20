using Eventinho.Domain.Enums;

namespace Eventinho.Domain.Entities
{
    public class Guest : Entity
    {
        public Event Event { get; set; }
        public Guid EventId { get; set; }

        public User User { get; set; }
        public Guid UserId { get; set; }

        public EState State { get; set; }
    }
}
