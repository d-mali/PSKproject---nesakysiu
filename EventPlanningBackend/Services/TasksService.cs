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

        public async Task<TaskResponse> CreateTaskAsync(TaskRequest taskRequest)
        {
            //before creating a task, we need to check if the event exists
            var eventEntity = await _context.Events.AnyAsync(e => e.Id == taskRequest.EventId);
            if (!eventEntity)
                throw new Exception("Event not found");

            var task = await _context.Tasks.AddAsync(new EventTask(taskRequest));

            await _context.SaveChangesAsync();

            return task.Entity.ToResponse();
        }

        public async Task<bool> DeleteTaskAsync(Guid id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
                return false;

            _context.Tasks.Remove(task);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<TaskResponse>> GetTasksAsync(Expression<Func<EventTask, bool>>? filter = null, Func<IQueryable<EventTask>, IOrderedQueryable<EventTask>>? orderBy = null, int? itemsToSkip = null, int? itemsToTake = null)
        {
            var tasks = await _context.Tasks.ToListAsync();

            return tasks.Select(t => t.ToResponse());
        }

        public async Task<TaskResponse> UpdateTaskAsync(Guid id, TaskRequest task)
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

        public async Task<TaskResponse?> GetTaskAsync(Guid taskId)
        {
            var task = await _context.Tasks.FindAsync(taskId);

            if (task == null)
                return null;

            return task.ToResponse();
        }
    }
}
