using EventBackend.Services.Interfaces;
using EventDataAccess.Abstractions;
using EventDomain.Entities;
using System.Linq.Expressions;



namespace EventBackend.Services
{
    public class TasksService : ITasksService
    {
        private readonly IGenericRepository<EventTask> _taskRepository;

        public TasksService(IGenericRepository<EventTask> taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public Task<EventTask> CreateTask(Guid eventId, EventTask task)
        {
            throw new NotImplementedException();
        }

        public async Task<EventTask> CreateTaskAsync(EventTask task)
        {
            return await _taskRepository.InsertAsync(task);
        }

        public async Task<bool> DeleteTask(Guid id)
        {
            return await _taskRepository.DeleteAsync(id);
        }

        public Task<bool> DeleteTask(Guid eventId, Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EventTask>> GetAllTasksAsync(Expression<Func<EventTask, bool>>? filter = null, Func<IQueryable<EventTask>, IOrderedQueryable<EventTask>>? orderBy = null, int? itemsToSkip = null, int? itemsToTake = null)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<EventTask>> GetEventTasksAsync(Guid eventId)
        {
            var tasks = await _taskRepository.GetAllAsync();

            return tasks.Where(x => x.EventId == eventId);
        }

        public Task<EventTask> GetTask(Guid eventId, Guid taskId)
        {
            throw new NotImplementedException();
        }

        public Task<EventTask> GetTaskByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EventTask>> GetTasks(Guid eventId, Expression<Func<EventTask, bool>>? filter = null, Func<IQueryable<EventTask>, IOrderedQueryable<EventTask>>? orderBy = null, int? itemsToSkip = null, int? itemsToTake = null)
        {
            throw new NotImplementedException();
        }

        public async Task<EventTask> UpdateTask(Guid id, EventTask entity)
        {
            return await _taskRepository.UpdateAsync(entity);
        }

        public Task<EventTask> UpdateTask(Guid eventId, Guid id, EventTask task)
        {
            throw new NotImplementedException();
        }
    }
}
