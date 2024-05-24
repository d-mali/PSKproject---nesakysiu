using EventDomain.Entities;

namespace EventBackend.Services.Interfaces
{
    public interface IEventTaskService
    {
        public Task<EventTask> CreateTaskAsync(EventTask entity);
        public Task<IEnumerable<EventTask>> GetAllTasksAsync();
        public Task<EventTask> GetTaskByIdAsync(Guid id);
        public Task<EventTask> UpdateTaskAsync(Guid id, EventTask entity);
        public Task<bool> DeleteTaskAsync(Guid id);
    }
}
