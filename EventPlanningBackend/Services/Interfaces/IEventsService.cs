using EventBackend.Entities;
using EventBackend.Filters;
using EventDomain.Contracts.Requests;
using static EventBackend.Controllers.EventsController;

namespace EventBackend.Services.Interfaces
{
    public interface IEventsService
    {
        public Task<Event?> CreateEventAsync(EventRequest entity);
        public Task<IEnumerable<Event>> GetAllEventsAsync(EventsQuery filter);
        public Task<Event?> GetEventByIdAsync(Guid id);
        public Task<Event?> UpdateEventAsync(Guid id, EventRequest entity);

        public Task<Event?> CreateParticipation(ParticipationRequest request);

        public Task<List<Participant>?> GetEventParticipants(Guid id);
        public Task<bool> DeleteEventAsync(Guid id);
    }
}
