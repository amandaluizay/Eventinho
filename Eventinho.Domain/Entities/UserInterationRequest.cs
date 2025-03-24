using Eventinho.Domain.Enums;

namespace Eventinho.Domain.Entities
{
    public class UserInterationRequest : Entity
    {
        public User UserSender { get; set; }
        public Guid UserSenderId { get; set; }

        public User UserReceiver { get; set; }
        public Guid UserReceiverId { get; set; }

        public EInterationType Type { get; set; }

        public EState State { get; set; }
    }
}
