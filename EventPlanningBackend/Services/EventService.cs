using EventBackend.Services.Interfaces;
using EventDataAccess.Abstractions;
using EventDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<Event> CreateEventAsync(Event entity)
        {
            return await _eventRepository.InsertAsync(entity);
        }

        public async Task<bool> DeleteEventAsync(Guid id)
        {
            var eventEntity = await _eventRepository.GetByIdAsync(id);

            if (eventEntity == null)
                return false;

            var result = await _eventRepository.DeleteAsync(eventEntity);

            return result;
        }

        public Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Event> GetEventByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Event> UpdateEventAsync(Guid id, Event entity)
        {
            throw new NotImplementedException();
        }

        public Task<Entities.Task> GetAllTasksAsync()
        {
            throw new NotImplementedException();
        }
    }
}
