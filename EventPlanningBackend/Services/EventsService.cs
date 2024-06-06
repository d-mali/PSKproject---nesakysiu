using EventBackend.Entities;
using EventBackend.Filters;
using EventBackend.Services.Interfaces;
using EventDataAccess.Abstractions;
using EventDomain.Contracts.Requests;
using EventDomain.Contracts.Responses;
using Microsoft.EntityFrameworkCore;

namespace EventBackend.Services
{
    public class EventsService(IGenericRepository<Event> eventRepository, MainDbContext context)
        : IEventsService
    {
        protected readonly MainDbContext Context = context;

        public async Task<EventResponse?> CreateEventAsync(EventRequest eventRequest)
        {
            var eventEntity = new Event(eventRequest);

            var events = await eventRepository.InsertAsync(eventEntity);

            await Context.SaveChangesAsync();

            return events.ToResponse();
        }

        public async Task<bool> DeleteEventAsync(Guid eventId)
        {
            var eventEntity = await eventRepository.GetByIdAsync(eventId);

            if (eventEntity == null)
                return false;

            var result = await eventRepository.DeleteAsync(eventId);

            return result;
        }

        public async Task<IEnumerable<EventResponse>> GetAllEventsAsync(EventsQuery filter)
        {
            var eventFilter = PredicateBuilder.True<Event>();
            Func<IQueryable<Event>, IOrderedQueryable<Event>>? orderByEvent = null;

            if (filter.Title != null)
            {
                eventFilter = eventFilter.And(x => x.Title == filter.Title);
            }

            if (filter.MinDate != null)
            {
                eventFilter = eventFilter.And(x => x.StartDate == filter.MinDate);
            }

            if (filter.MaxDate != null)
            {
                eventFilter = eventFilter.And(x => x.EndDate == filter.MaxDate);
            }

            switch (filter.Sort)
            {
                case Sorting.Desc:
                    orderByEvent = x => x.OrderByDescending(p => EF.Property<Event>(p, filter.OrderBy.ToString()));
                    break;
                default:
                    orderByEvent = x => x.OrderBy(p => EF.Property<Event>(p, filter.OrderBy.ToString()));
                    break;
            }

            var events = await eventRepository.GetAllAsync(eventFilter, orderByEvent, filter.Skip, filter.Take);

            return events.Select(x => x.ToResponse());
        }

        public async Task<EventResponse?> GetEventAsync(Guid id)
        {
            var eventEntity = await eventRepository.GetByIdAsync(id);

            return eventEntity?.ToResponse();
        }

        public async Task<EventResponse?> UpdateEventAsync(Guid id, EventRequest eventRequest)
        {
            var eventEntity = await eventRepository.GetByIdAsync(id);
            if (eventEntity == null)
                return null;

            eventEntity.Title = eventRequest.Title;
            eventEntity.StartDate = eventRequest.StartDate;
            eventEntity.EndDate = eventRequest.EndDate;
            eventEntity.Description = eventRequest.Description;


            await Context.SaveChangesAsync();

            return eventEntity.ToResponse();
        }

        public async Task<EventResponse?> CreateParticipation(Guid eventId, Guid participantId)
        {
            var eventEntity = await Context.Events.FindAsync(eventId);
            var participant = await Context.Participants.FindAsync(participantId);

            if (eventEntity == null || participant == null)
            {
                return eventEntity?.ToResponse();
            }

            eventEntity.Participants.Add(participant);

            await Context.SaveChangesAsync();

            return eventEntity.ToResponse();
        }

        public async Task<EventResponse?> DeleteParticipation(Guid eventId, Guid participantId)
        {
            var eventEntity = await Context.Events
               .Include(e => e.Participants)
               .FirstOrDefaultAsync(e => e.Id == eventId);

            var participant = eventEntity?.Participants.FirstOrDefault(p => p.Id == participantId);

            if (participant == null)
            {
                return null;
            }

            if (eventEntity == null)
            {
                return null;
            }

            eventEntity.Participants.Remove(participant);

            await Context.SaveChangesAsync();

            return eventEntity.ToResponse();
        }

        public async Task<IEnumerable<Participant>?> GetEventParticipants(Guid id)
        {
            var eventWithParticipants = await Context.Events
                .Include(s => s.Participants)
                .FirstOrDefaultAsync(s => s.Id == id);

            var participants = eventWithParticipants?.Participants.ToList();

            return participants;
        }

        public async Task<EventResponse?> CreateWorker(Guid eventId, string userId)
        {
            var eventEntity = await Context.Events.FindAsync(eventId);
            var worker = await Context.Users.FindAsync(userId);

            if (eventEntity == null || worker == null)
            {
                return null;
            }

            eventEntity.Users.Add(worker);

            await Context.SaveChangesAsync();

            return eventEntity.ToResponse();
        }

        public async Task<IEnumerable<ApplicationUser>?> GetEventWorkers(Guid id)
        {
            var eventWithWorkers = await Context.Events
                .Include(s => s.Users)
                .FirstOrDefaultAsync(s => s.Id == id);

            var workers = eventWithWorkers?.Users.ToList();

            return workers;
        }

        public async Task<EventResponse?> DeleteWorker(Guid eventId, string userId)
        {
            var eventEntity = await Context.Events
               .Include(e => e.Users)
               .FirstOrDefaultAsync(e => e.Id == eventId);

            var participant = eventEntity?.Users.FirstOrDefault(p => p.Id == userId);

            if (participant == null)
            {
                return null;
            }

            if (eventEntity == null)
            {
                return null;
            }

            eventEntity.Users.Remove(participant);

            await Context.SaveChangesAsync();

            return eventEntity.ToResponse();
        }
    }
}
