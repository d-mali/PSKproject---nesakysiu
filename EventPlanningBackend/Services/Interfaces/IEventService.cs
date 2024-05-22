using EventDomain.Entities;

namespace EventBackend.Services.Interfaces
{
    public interface IEventService
    {
        public Task<Event> CreateEventAsync(Event entity);
        public Task<IEnumerable<Event>> GetAllEventsAsync();
        public Task<Event> GetEventByIdAsync(Guid id);
        public Task<Event> UpdateEventAsync(Guid id, Event entity);
        public Task<bool> DeleteEventAsync(Guid id);
    }
}
