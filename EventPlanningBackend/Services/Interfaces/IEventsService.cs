using EventBackend.Entities;
using EventBackend.Filters;
using EventDomain.Contracts.Requests;
using EventDomain.Contracts.Responses;

namespace EventBackend.Services.Interfaces
{
    public interface IEventsService
    {
        public Task<EventResponse?> CreateEventAsync(EventRequest entity);
        public Task<IEnumerable<EventResponse>> GetAllEventsAsync(EventsQuery filter);
        public Task<EventResponse?> GetEventAsync(Guid id);
        public Task<EventResponse?> UpdateEventAsync(Guid id, EventRequest entity);
        public Task<EventResponse?> CreateParticipation(Guid eventId, Guid participantId);
        public Task<EventResponse?> DeleteParticipation(Guid eventId, Guid participantId);
        public Task<IEnumerable<Participant>?> GetEventParticipants(Guid eventId);
        public Task<bool> DeleteEventAsync(Guid eventId);
        public Task<IEnumerable<ApplicationUser>?> GetEventWorkers(Guid id);
        public Task<EventResponse?> CreateWorker(Guid eventId, string userId);
        public Task<EventResponse?> DeleteWorker(Guid eventId, string userId);
    }
}
