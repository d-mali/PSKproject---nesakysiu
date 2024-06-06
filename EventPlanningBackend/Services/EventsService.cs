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

        public async Task<bool> DeleteEventAsync(Guid id)
        {
            var eventEntity = await eventRepository.GetByIdAsync(id);

            if (eventEntity == null)
                return false;

            var result = await eventRepository.DeleteAsync(id);

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
            var eventas = await Context.Events.FindAsync(eventId);
            var participant = await Context.Participants.FindAsync(participantId);

            if (eventas == null || participant == null)
            {
                return eventas?.ToResponse();
            }
            eventas.Participants ??= new List<Participant>();

            eventas.Participants.Add(participant);

            await Context.SaveChangesAsync();

            return eventas.ToResponse();
        }

        public async Task<EventResponse?> DeleteParticipation(Guid eventId, Guid participantId)
        {
            var eventas = await Context.Events
               .Include(e => e.Participants)
               .FirstOrDefaultAsync(e => e.Id == eventId);
            if (eventas == null)
            {
                return null;
            }
            if (eventas.Participants == null)
            {
                return null;
            }

            var participant = eventas.Participants.FirstOrDefault(p => p.Id == participantId);
            if (participant == null)
            {
                return null;
            }


            eventas.Participants ??= new List<Participant>();

            eventas.Participants.Remove(participant);

            await Context.SaveChangesAsync();

            return eventas.ToResponse();
        }

        public async Task<IEnumerable<Participant>?> GetEventParticipants(Guid id)
        {
            var eventWithParticipants = await Context.Events
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

        public async Task<EventResponse?> CreateWorker(Guid eventId, String userId)
        {
            var eventas = await Context.Events.FindAsync(eventId);
            var worker = await Context.Users.FindAsync(userId);

            if (eventas == null || worker == null)
            {
                return null;
            }
            eventas.Users ??= new List<ApplicationUser>();

            eventas.Users.Add(worker);

            await Context.SaveChangesAsync();

            return eventas.ToResponse();
        }

        public async Task<IEnumerable<ApplicationUser>?> GetEventWorkers(Guid id)
        {
            var eventWithWorkers = await Context.Events
                .Include(s => s.Users)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (eventWithWorkers == null)
            {
                return null;
            }
            if (eventWithWorkers.Users == null)
            {
                return null;
            }

            var workers = eventWithWorkers.Users.ToList();

            return workers;
        }

        public async Task<EventResponse?> DeleteWorker(Guid eventId, String userId)
        {
            var eventas = await Context.Events
               .Include(e => e.Users)
               .FirstOrDefaultAsync(e => e.Id == eventId);
            if (eventas == null)
            {
                return null;
            }
            if (eventas.Users == null)
            {
                return null;
            }

            var participant = eventas.Users.FirstOrDefault(p => p.Id == userId);
            if (participant == null)
            {
                return null;
            }


            eventas.Users ??= new List<ApplicationUser>();

            eventas.Users.Remove(participant);

            await Context.SaveChangesAsync();

            return eventas.ToResponse();
        }
    }
}
