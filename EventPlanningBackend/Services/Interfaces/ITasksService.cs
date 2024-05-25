using EventDomain.Entities;
using System.Linq.Expressions;

namespace EventBackend.Services.Interfaces
{
    public interface ITasksService
    {
        public Task<EventTask> CreateTask(Guid eventId, EventTask task);

        public Task<IEnumerable<EventTask>> GetTasks(
            Guid eventId,
            Expression<Func<EventTask, bool>>? filter = null,
            Func<IQueryable<EventTask>, IOrderedQueryable<EventTask>>? orderBy = null,
            int? itemsToSkip = null,
            int? itemsToTake = null);

        public Task<EventTask> GetTask(Guid eventId, Guid taskId);
        public Task<EventTask> UpdateTask(Guid eventId, Guid id, EventTask task);
        public Task<bool> DeleteTask(Guid eventId, Guid id);
    }
}
