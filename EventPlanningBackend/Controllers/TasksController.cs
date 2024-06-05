using EventBackend.Services.Interfaces;
using EventDomain.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;


namespace EventBackend.Controllers
{
    [ApiController]
    [Route("api/Events/{eventId}/[controller]")]
    [ApiExplorerSettings(GroupName = "Events")]
    public class TasksController : ControllerBase
    {
        private readonly ITasksService _taskService;

        public TasksController(ITasksService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTasks(Guid eventId)
        {
            var tasks = await _taskService.GetTasksAsync(eventId);

            return Ok(tasks);
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
            var createdTask = await _taskService.CreateTaskAsync(taskRequest);

            //return CreatedAtAction(nameof(CreateTask),
            //    createdTask
            //    );
            return Ok(createdTask);
        }

        // PUT: api/Tasks/5
        [HttpPut("{taskId}")]
        public async Task<IActionResult> UpdateTask(Guid eventId, Guid taskId, TaskRequest taskRequest)
        {
            var updatedTask = await _taskService.UpdateTaskAsync(taskId, taskRequest);

            return Ok(updatedTask);
        }

        // DELETE: api/Tasks/5
        [HttpDelete("{taskId}")]
        public async Task<IActionResult> DeleteTask(Guid eventId, Guid taskId)
        {
            await _taskService.DeleteTaskAsync(taskId);

            return NoContent();
        }


    }
}

