using EventinhoApplication.Models;

namespace EventinhoApplication.Interfaces
{
    public interface IEventService
    {
        Task CreateEventAsync(EventModel eventModel, string userEmail);
    }
}