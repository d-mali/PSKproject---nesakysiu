using EventBackend.Filters;
using EventBackend.Models.Requests;
using EventBackend.Services.Interfaces;
using EventDataAccess.Abstractions;
using EventDomain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventDomain.Services
{
    public class EventService : IEventService
    {
        private readonly IGenericRepository<Event> _eventRepository;

        public EventService(IGenericRepository<Event> eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<Event?> CreateEventAsync(EventRequest entity)
        {
            var evt = new Event
            {
                Title = entity.Title,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                Description = entity.Description
            };

            return await _eventRepository.InsertAsync(evt);
        }

        public async Task<bool> DeleteEventAsync(Guid id)
        {
            var eventEntity = await _eventRepository.GetByIdAsync(id);

            if (eventEntity == null)
                return false;

            var result = await _eventRepository.DeleteAsync(id);

            return result;
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync(EventsQuery filter)
        {
            var eventFilter = PredicateBuilder.True<Event>();
            Func<IQueryable<Event>, IOrderedQueryable<Event>>? orderByEvent = null;

            if (filter.Title != null)
            {
                eventFilter = eventFilter.And(x => x.Title == filter.Title);
            }

            if (filter.StartDate != null)
            {
                eventFilter = eventFilter.And(x => x.StartDate == filter.StartDate);
            }

            if (filter.EndDate != null)
            {
                eventFilter = eventFilter.And(x => x.EndDate == filter.EndDate);
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

            return await _eventRepository.GetAllAsync(eventFilter, orderByEvent, filter.ItemsToSkip(), filter.PageSize);
        }

        public async Task<Event?> GetEventByIdAsync(Guid id)
        {
            return await _eventRepository.GetByIdAsync(id);
        }

        public async Task<Event?> UpdateEventAsync(Guid id, EventRequest entity)
        {
            var evt = new Event
            {
                Id = id,
                Title = entity.Title,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                Description = entity.Description
            };

            return await _eventRepository.UpdateAsync(evt);
        }

    }
}
