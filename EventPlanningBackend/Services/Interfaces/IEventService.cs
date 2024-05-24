using EventBackend.Filters;
using EventBackend.Models.Requests;
using EventDomain.Entities;
using System.Linq.Expressions;

namespace EventBackend.Services.Interfaces
{
    public interface IEventService
    {
        public Task<Event?> CreateEventAsync(EventRequest entity);
        public Task<IEnumerable<Event>> GetAllEventsAsync(EventFilter filter);
        public Task<Event?> GetEventByIdAsync(Guid id);
        public Task<Event?> UpdateEventAsync(Guid id, EventRequest entity);
        public Task<bool> DeleteEventAsync(Guid id);
    }
}
