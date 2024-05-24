using EventBackend.Models.Requests;
using EventDomain.Entities;
using System.Linq.Expressions;

namespace EventBackend.Services.Interfaces
{
    public interface IEventService
    {
        public Task<Event?> CreateEventAsync(EventRequest entity);
        public Task<IEnumerable<Event>> GetAllEventsAsync(
            Expression<Func<Event, bool>>? filter = null,
            Func<IQueryable<Event>, IOrderedQueryable<Event>>? orderBy = null,
            int? itemsToSkip = null,
            int? itemsToTake = null);
        public Task<Event?> GetEventByIdAsync(Guid id);
        public Task<Event?> UpdateEventAsync(Guid id, EventRequest entity);
        public Task<bool> DeleteEventAsync(Guid id);
    }
}
