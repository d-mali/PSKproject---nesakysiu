using EventBackend.Entities;
using EventBackend.Models.Requests;
using EventBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace EventBackend.Controllers
{
    [Route("api/Events/{eventId}/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITasksService _taskService;

        public TasksController(ITasksService taskService)
        {
            _taskService = taskService;
        }

        // GET: api/Tasks
        [HttpGet]
        public async Task<IActionResult> GetTasks(Guid eventId)
        {
            return Ok(await _taskService.GetTasks(eventId));
        }

        // GET: api/Tasks/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTask(Guid eventId, Guid taskId)
        {
            return Ok(await _taskService.GetTask(eventId, taskId));
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(Guid eventId, TaskRequest taskRequest)
        {
            var task = new EventTask
            {
                Title = taskRequest.Title,
                ScheduledTime = taskRequest.ScheduledTime,
                Description = taskRequest.Description
            };

            var createdTask = await _taskService.CreateTask(eventId, task);

            //return CreatedAtAction(nameof(CreateTask),
            //    createdTask
            //    );
            return NoContent();
        }

        // PUT: api/Tasks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(Guid eventId, Guid id, TaskRequest taskRequest)
        {
            var task = new EventTask
            {
                Title = taskRequest.Title,
                ScheduledTime = taskRequest.ScheduledTime,
                Description = taskRequest.Description
            };

            var createdTask = await _taskService.UpdateTask(eventId, id, task);

            return Ok(createdTask);
        }

        // DELETE: api/Tasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(Guid eventId, Guid taskId)
        {
            await _taskService.DeleteTask(eventId, taskId);

            return NoContent();
        }
    }
}

