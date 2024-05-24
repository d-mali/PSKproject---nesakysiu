using EventBackend.Models.Requests;
using EventBackend.Services.Interfaces;
using EventDomain.Entities;
using System.Linq.Expressions;



namespace EventBackend.Services
{
    public class EventTaskService : IEventTaskService
    {
        public Task<EventTask> CreateTaskAsync(EventTaskRequest entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteTaskAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EventTask>> GetAllTasksAsync(
            Expression<Func<EventTask, bool>>? filter = null,
            Func<IQueryable<EventTask>, IOrderedQueryable<EventTask>>? orderBy = null,
            int? itemsToSkip = null,
            int? itemsToTake = null)
        {
            throw new NotImplementedException();
        }

        public Task<EventTask> GetTaskByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<EventTask> UpdateTaskAsync(Guid id, EventTaskRequest entity)
        {
            throw new NotImplementedException();
        }
    }
}
