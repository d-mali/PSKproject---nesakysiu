using EventBackend.Entities;
using EventBackend.Filters;
using EventBackend.Services.Interfaces;
using EventDataAccess.Abstractions;
using EventDomain.Contracts.Requests;
using EventDomain.Contracts.Responses;
using Microsoft.EntityFrameworkCore;

namespace EventBackend.Services
{
    public class EventsService : IEventsService
    {
        private readonly IGenericRepository<Event> _eventRepository;
        protected readonly MainDbContext _context;

        public EventsService(IGenericRepository<Event> eventRepository, MainDbContext context)
        {
            _eventRepository = eventRepository;
            _context = context;
        }

        public async Task<EventResponse?> CreateEventAsync(EventRequest eventRequest)
        {
            var eventEntity = new Event(eventRequest);

            var events = await _eventRepository.InsertAsync(eventEntity);

            await _context.SaveChangesAsync();

            return events.ToResponse();
        }

        public async Task<bool> DeleteEventAsync(Guid id)
        {
            var eventEntity = await _eventRepository.GetByIdAsync(id);

            if (eventEntity == null)
                return false;

            var result = await _eventRepository.DeleteAsync(id);

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

            var events = await _eventRepository.GetAllAsync(eventFilter, orderByEvent, filter.Skip, filter.Take);

            return events.Select(x => x.ToResponse());
        }

        public async Task<EventResponse?> GetEventAsync(Guid id)
        {
            var eventEntity = await _eventRepository.GetByIdAsync(id);

            return eventEntity?.ToResponse();
        }

        public async Task<EventResponse?> UpdateEventAsync(Guid id, EventRequest eventRequest)
        {
            var eventEntity = await _eventRepository.GetByIdAsync(id);
            if (eventEntity == null)
                return null;

            eventEntity.Title = eventRequest.Title;
            eventEntity.StartDate = eventRequest.StartDate;
            eventEntity.EndDate = eventRequest.EndDate;
            eventEntity.Description = eventRequest.Description;

            await _context.SaveChangesAsync();

            return eventEntity.ToResponse();
        }

        public async Task<EventResponse?> CreateParticipation(Guid eventId, Guid participantId)
        {
            var eventas = await _context.Events.FindAsync(eventId);
            var participant = await _context.Participants.FindAsync(participantId);

            if (eventas == null || participant == null)
            {
                return eventas?.ToResponse();
            }
            eventas.Participants ??= new List<Participant>();

            eventas.Participants.Add(participant);

            await _context.SaveChangesAsync();

            return eventas.ToResponse();
        }

        public async Task<IEnumerable<Participant>?> GetEventParticipants(Guid id)
        {
            var eventWithParticipants = await _context.Events
                .Include(s => s.Participants)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (eventWithParticipants == null)
            {
                return null;
            }
            if (eventWithParticipants.Participants == null)
            {
                return null;
            }

            var participants = eventWithParticipants.Participants.ToList();

            return participants;
        }
    }
}
