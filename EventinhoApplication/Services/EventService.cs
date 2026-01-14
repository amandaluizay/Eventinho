using Eventinho.Domain.Entities;
using Eventinho.Domain.Interfaces.Repository;
using Eventinho.Shared.Interfaces;
using EventinhoApplication.Interfaces;
using EventinhoApplication.Models;

namespace EventinhoApplication.Services
{
    public class EventService : IEventService
    {
        private readonly IRepository<Event> _eventRepository;
        private readonly IUserService _userService;

        public EventService(IRepository<Event> repository, IUserService userService)
        {
            _eventRepository = repository; 
            _userService = userService;
        }
        public async Task CreateEventAsync(EventModel eventModel, string userEmail)
        {
            if(string.IsNullOrEmpty(eventModel.Description))
            {
                throw new Exception("Event description cannot be empty");
            }

            if (eventModel.StartDate < DateTime.UtcNow) 
            {
                throw new Exception("Start date has an invalid date");
            }

            if (eventModel.EndDate != null && eventModel.EndDate < DateTime.UtcNow)
            {
                throw new Exception("End date has an invalid date");
            }

            if (eventModel.Limit != null && eventModel.Limit <= 0)
            {
                throw new Exception("limit has to be greater than 0");
            }

            var user = await _userService.GetUserByEmailAsync(userEmail);

            var eventEntity = new Event
            {
                Id = Guid.NewGuid(),
                StartDate = eventModel.StartDate,
                Description = eventModel.Description,
                Local = eventModel.Local,
                Limit = eventModel.Limit,
                EndDate = eventModel.EndDate,
                OwnerId = user.Id,
            };

            await _eventRepository.AddAsync(eventEntity);
           
        }
    }
}
