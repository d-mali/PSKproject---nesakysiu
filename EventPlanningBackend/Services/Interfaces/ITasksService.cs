using EventBackend.Entities;
using EventDomain.Contracts.Requests;
using EventDomain.Contracts.Responses;
using System.Linq.Expressions;

namespace EventBackend.Services.Interfaces
{
    public interface ITasksService
    {
        public Task<TaskResponse> CreateTaskAsync(TaskRequest task);

        public Task<IEnumerable<TaskResponse>> GetTasksAsync(
            Guid eventId,
            Expression<Func<EventTask, bool>>? filter = null,
            Func<IQueryable<EventTask>, IOrderedQueryable<EventTask>>? orderBy = null,
            int? itemsToSkip = null,
            int? itemsToTake = null);

        public Task<TaskResponse?> GetTaskAsync(Guid eventId, Guid taskId);
        public Task<TaskResponse> UpdateTaskAsync(Guid id, TaskRequest task);
        public Task<bool> DeleteTaskAsync(Guid id);
    }
}
