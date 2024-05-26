using EventBackend.Entities;
using EventBackend.Services.Interfaces;
using EventDomain.Contracts.Requests;
using EventDomain.Contracts.Responses;
using Microsoft.EntityFrameworkCore;
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

        public async Task<TaskResponse> CreateTask(Guid eventId, TaskRequest taskRequest)
        {
            var task = new EventTask(taskRequest);

            var eventEntity = await _context.Events.FindAsync(eventId);

            if (eventEntity == null)
            {
                throw new Exception("Event not found");
            }

            eventEntity.Tasks.Add(task);

            await _context.SaveChangesAsync();

            return task.ToResponse();
        }

        public async Task<bool> DeleteTaskAsync(Guid eventId, Guid id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
                return false;

            _context.Tasks.Remove(task);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<TaskResponse>> GetTasksAsync(Guid eventId, Expression<Func<EventTask, bool>>? filter = null, Func<IQueryable<EventTask>, IOrderedQueryable<EventTask>>? orderBy = null, int? itemsToSkip = null, int? itemsToTake = null)
        {
            var eventEntity = await _context.Events.Include(e => e.Tasks).FirstOrDefaultAsync(e => e.Id == eventId);

            if (eventEntity == null)
            {
                throw new Exception("Event not found");
            }

            return eventEntity.Tasks.Select(t => t.ToResponse());
        }

        public async Task<TaskResponse> UpdateTaskAsync(Guid eventId, Guid id, TaskRequest task)
        {
            var taskEntity = await _context.Tasks.FindAsync(id);
            if (taskEntity == null)
                throw new Exception("Task not found");

            taskEntity.Title = task.Title;
            taskEntity.ScheduledTime = task.ScheduledTime;
            taskEntity.Description = task.Description;

            await _context.SaveChangesAsync();

            return taskEntity.ToResponse();
        }

        public async Task<TaskResponse?> GetTaskAsync(Guid eventId, Guid taskId)
        {
            var task = await _context.Tasks.FindAsync(taskId);

            if (task == null)
                return null;

            return task.ToResponse();
        }
    }
}
