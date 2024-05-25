using EventBackend.Filters;
using EventBackend.Models.Requests;
using EventBackend.Entities;

namespace EventBackend.Services.Interfaces
{
    public interface IEventsService
    {
        public Task<Event?> CreateEventAsync(EventRequest entity);
        public Task<IEnumerable<Event>> GetAllEventsAsync(EventsQuery filter);
        public Task<Event?> GetEventByIdAsync(Guid id);
        public Task<Event?> UpdateEventAsync(Guid id, EventRequest entity);
        public Task<bool> DeleteEventAsync(Guid id);
    }
}
