using EventBackend.Entities;
using EventBackend.Services.Interfaces;
using System.Linq.Expressions;



namespace EventBackend.Services
{
    public class TasksService : ITasksService
    {
        private readonly MainDbContext _context;

        public TasksService(MainDbContext context)
        {
            _context = context;
        }

        public async Task<EventTask> CreateTask(Guid eventId, EventTask task)
        {
            var eventEntity = _context.Events.Find(eventId);

            if (eventEntity == null)
            {
                throw new Exception("Event not found");
            }

            eventEntity.Tasks.Add(task);

            await _context.SaveChangesAsync();

            return task;
        }

        public Task<EventTask> CreateTaskAsync(EventTask task)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteTask(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteTask(Guid eventId, Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EventTask>> GetAllTasksAsync(Expression<Func<EventTask, bool>>? filter = null, Func<IQueryable<EventTask>, IOrderedQueryable<EventTask>>? orderBy = null, int? itemsToSkip = null, int? itemsToTake = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EventTask>> GetEventTasksAsync(Guid eventId)
        {
            throw new NotImplementedException();
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

        public Task<EventTask> UpdateTask(Guid id, EventTask entity)
        {
            throw new NotImplementedException();
        }

        public Task<EventTask> UpdateTask(Guid eventId, Guid id, EventTask task)
        {
            throw new NotImplementedException();
        }
    }
}
