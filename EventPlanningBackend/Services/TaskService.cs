using EventBackend.Services.Interfaces;

namespace EventBackend.Services
{
    public class TaskService : ITaskService
    {
        public Task<EventDomain.Entities.Task> CreateTaskAsync(EventDomain.Entities.Task entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteTaskAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EventDomain.Entities.Task>> GetAllTasksAsync()
        {
            throw new NotImplementedException();
        }

        public Task<EventDomain.Entities.Task> GetTaskByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<EventDomain.Entities.Task> UpdateTaskAsync(Guid id, EventDomain.Entities.Task entity)
        {
            throw new NotImplementedException();
        }
    }
}
