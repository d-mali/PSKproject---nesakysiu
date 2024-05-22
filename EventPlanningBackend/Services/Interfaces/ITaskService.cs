using EventDomain.Entities;

namespace EventBackend.Services.Interfaces
{
    public interface ITaskService
    {
        public Task<EventDomain.Entities.Task> CreateTaskAsync(EventDomain.Entities.Task entity);
        public Task<IEnumerable<EventDomain.Entities.Task>> GetAllTasksAsync();
        public Task<EventDomain.Entities.Task> GetTaskByIdAsync(Guid id);
        public Task<EventDomain.Entities.Task> UpdateTaskAsync(Guid id, EventDomain.Entities.Task entity);
        public Task<bool> DeleteTaskAsync(Guid id);
    }
}
