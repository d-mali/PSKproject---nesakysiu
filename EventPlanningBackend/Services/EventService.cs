using EventBackend.Models.Requests;
using EventBackend.Services.Interfaces;
using EventDataAccess.Abstractions;
using EventDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<Event>> GetAllEventsAsync(
            Expression<Func<Event, bool>>? filter = null,
            Func<IQueryable<Event>, IOrderedQueryable<Event>>? orderBy = null,
            int? itemsToSkip = null,
            int? itemsToTake = null)
        {
            return await _eventRepository.GetAllAsync(filter, orderBy, itemsToSkip, itemsToTake);
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

        //public async Task<EventTask> GetAllTasksAsync()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
