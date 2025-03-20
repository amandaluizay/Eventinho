using Eventinho.Domain.Enums;

namespace Eventinho.Domain.Entities
{
    public class UserInterationRequest : Entity
    {
        public User User_Sender { get; set; }
        public Guid User_SenderId { get; set; }

        public User User_Receiver { get; set; }
        public Guid User_ReceiverId { get; set; }

        public EInterationType Type { get; set; }
    }
}
