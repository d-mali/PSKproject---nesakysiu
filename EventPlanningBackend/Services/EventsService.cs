using EventBackend.Entities;
using EventBackend.Filters;
using EventBackend.Models.Requests;
using EventBackend.Services.Interfaces;
using EventDataAccess.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace EventBackend.Services
{
    public class EventsService : IEventsService
    {
        private readonly IGenericRepository<Event> _eventRepository;

        public EventsService(IGenericRepository<Event> eventRepository)
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

            return await _eventRepository.GetAllAsync(eventFilter, orderByEvent, filter.Skip, filter.Take);
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
