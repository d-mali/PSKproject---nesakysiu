using EventBackend.Services.Interfaces;
using EventDataAccess.Abstractions;
using EventDomain.Entities;
using System.Linq.Expressions;



namespace EventBackend.Services
{
    public class TaskService : ITaskService
    {
        private readonly IGenericRepository<EventTask> _taskRepository;

        public TaskService(IGenericRepository<EventTask> taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public Task<EventTask> CreateTaskAsync(EventTask task)
        {
            return _taskRepository.InsertAsync(task);
        }

        public Task<bool> DeleteTaskAsync(Guid id)
        {
            return _taskRepository.DeleteAsync(id);
        }

        public Task<IEnumerable<EventTask>> GetAllTasksAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EventTask>> GetAllTasksAsync(Expression<Func<EventTask, bool>>? filter = null, Func<IQueryable<EventTask>, IOrderedQueryable<EventTask>>? orderBy = null, int? itemsToSkip = null, int? itemsToTake = null)
        {
            throw new NotImplementedException();
        }

        public Task<EventTask> GetTaskByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<EventTask> UpdateTaskAsync(Guid id, EventTask entity)
        {
            return _taskRepository.UpdateAsync(entity);
        }
    }
}
