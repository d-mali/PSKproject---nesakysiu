using EventBackend.Services.Interfaces;
using EventDomain.Entities;



namespace EventBackend.Services
{
    public class EventTaskService : IEventTaskService
    {
        public Task<EventTask> CreateTaskAsync(EventTask entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteTaskAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EventTask>> GetAllTasksAsync()
        {
            throw new NotImplementedException();
        }

        public Task<EventTask> GetTaskByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<EventTask> UpdateTaskAsync(Guid id, EventTask entity)
        {
            throw new NotImplementedException();
        }
    }
}
