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

        public Task<Event> CreateEventAsync(Event entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteEventAsync(Guid id)
        {
            throw new NotImplementedException();
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
