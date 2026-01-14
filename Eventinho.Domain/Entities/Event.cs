namespace Eventinho.Domain.Entities
{
    public class Event : Entity
    {
        public string Description { get; set; }
        public string Local { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Limit { get; set; }
        
        public User Owner { get; set; }
        public Guid OwnerId { get; set; }

        public List<EventModerator>? Moderators { get; set; }
        public List<Guest>? Guests { get; set; }
    }
}
