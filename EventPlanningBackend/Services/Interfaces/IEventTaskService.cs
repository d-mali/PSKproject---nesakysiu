using EventBackend.Models.Requests;
using EventDomain.Entities;
using System.Linq.Expressions;

namespace EventBackend.Services.Interfaces
{
    public interface IEventTaskService
    {
        public Task<EventTask> CreateTaskAsync(EventTaskRequest entity);
        public Task<IEnumerable<EventTask>> GetAllTasksAsync(
            Expression<Func<EventTask, bool>>? filter = null,
            Func<IQueryable<EventTask>, IOrderedQueryable<EventTask>>? orderBy = null,
            int? itemsToSkip = null,
            int? itemsToTake = null);
        public Task<EventTask> GetTaskByIdAsync(Guid id);
        public Task<EventTask> UpdateTaskAsync(Guid id, EventTaskRequest entity);
        public Task<bool> DeleteTaskAsync(Guid id);
    }
}
