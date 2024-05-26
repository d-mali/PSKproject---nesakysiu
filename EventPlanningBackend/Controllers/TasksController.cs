using EventBackend.Services.Interfaces;
using EventDomain.Contracts.Requests;
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
            return Ok(await _taskService.GetTasksAsync(eventId));
        }

        // GET: api/Tasks/5
        [HttpGet("{taskId}")]
        public async Task<IActionResult> GetTask(Guid eventId, Guid taskId)
        {
            var task = await _taskService.GetTaskAsync(eventId, taskId);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(Guid eventId, TaskRequest taskRequest)
        {
            var createdTask = await _taskService.CreateTask(eventId, taskRequest);

            //return CreatedAtAction(nameof(CreateTask),
            //    createdTask
            //    );
            return Ok(createdTask);
        }

        // PUT: api/Tasks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(Guid eventId, Guid id, TaskRequest taskRequest)
        {
            var updatedTask = await _taskService.UpdateTaskAsync(eventId, id, taskRequest);

            return Ok(updatedTask);
        }

        // DELETE: api/Tasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(Guid eventId, Guid taskId)
        {
            await _taskService.DeleteTaskAsync(eventId, taskId);

            return NoContent();
        }
    }
}

