using EventDomain.Entities;
using System.Linq.Expressions;

namespace EventBackend.Services.Interfaces
{
    public interface ITaskService
    {
        public Task<EventTask> CreateTaskAsync(EventTask entity);
        public Task<IEnumerable<EventTask>> GetAllTasksAsync(
            Expression<Func<EventTask, bool>>? filter = null,
            Func<IQueryable<EventTask>, IOrderedQueryable<EventTask>>? orderBy = null,
            int? itemsToSkip = null,
            int? itemsToTake = null);
        public Task<EventTask> GetTaskByIdAsync(Guid id);
        public Task<EventTask> UpdateTaskAsync(Guid id, EventTask entity);
        public Task<bool> DeleteTaskAsync(Guid id);
        public Task<IEnumerable<EventTask>> GetEventTasksAsync(Guid eventId);
    }
}
