using EventBackend.Entities;
using EventBackend.Filters;
using EventDomain.Contracts.Requests;
using EventDomain.Contracts.Responses;
using static EventBackend.Controllers.EventsController;

namespace EventBackend.Services.Interfaces
{
    public interface IEventsService
    {
        public Task<EventResponse?> CreateEventAsync(EventRequest entity);
        public Task<IEnumerable<EventResponse>> GetAllEventsAsync(EventsQuery filter);
        public Task<EventResponse?> GetEventAsync(Guid id);
        public Task<EventResponse?> UpdateEventAsync(Guid id, EventRequest entity);
        public Task<EventResponse?> CreateParticipation(ParticipationRequest request);
        public Task<IEnumerable<Participant>?> GetEventParticipants(Guid id);
        public Task<bool> DeleteEventAsync(Guid id);
    }
}
