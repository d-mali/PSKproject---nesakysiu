using EventBackend.Models.Requests;
using EventBackend.Services.Interfaces;
using EventDomain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;


namespace EventPlanningBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        // GET: api/Tasks
        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            return Ok(await _taskService.GetAllTasksAsync());
        }

        // GET: api/Tasks/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTask(Guid id)
        {
            var entity = await _taskService.GetTaskByIdAsync(id);

            return Ok(entity);
        }

        // POST: api/Tasks
        [HttpPost]
        public async Task<IActionResult> CreateTask(EventTaskRequest taskRequest)
        {
            var task = new EventTask
            {
                Title = taskRequest.Title,
                ScheduledTime = taskRequest.ScheduledTime,
                Description = taskRequest.Description
            };
            var createdTask = await _taskService.CreateTaskAsync(task);

            return CreatedAtAction(nameof(CreateTask),
                createdTask
                );
        }

        // PUT: api/Tasks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask([FromRoute][Required] Guid id, EventTaskRequest taskRequest)
        {
            var task = new EventTask
            {
                Title = taskRequest.Title,
                ScheduledTime = taskRequest.ScheduledTime,
                Description = taskRequest.Description
            };

            var createdTask = await _taskService.UpdateTaskAsync(id, task);

            return Ok(createdTask);
        }

        // DELETE: api/Tasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            await _taskService.DeleteTaskAsync(id);

            return NoContent();
        }
    }
}

