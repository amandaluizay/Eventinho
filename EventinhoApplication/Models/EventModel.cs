using Eventinho.Domain.Entities;

namespace EventinhoApplication.Models
{
    public class EventModel
    {
        public string Description { get; set; }
        public string Local { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Limit { get; set; }
    }
}
