using EventBackend.Models.Requests;
using EventBackend.Services.Interfaces;
using EventDomain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;


namespace EventPlanningBackend.Controllers
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
            return Ok(await _taskService.GetAllTasksAsync());
        }

        // GET: api/Tasks/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTask(Guid eventId)
        {
            var entity = await _taskService.GetTaskByIdAsync(eventId);

            return Ok(entity);
        }

        // POST: api/Tasks
        [HttpPost]
        public async Task<IActionResult> CreateTask(Guid eventId, TaskRequest taskRequest)
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
        public async Task<IActionResult> UpdateTask(Guid eventId, [FromRoute][Required] Guid id, TaskRequest taskRequest)
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
        public async Task<IActionResult> DeleteTask(Guid eventId)
        {
            await _taskService.DeleteTaskAsync(eventId);

            return NoContent();
        }
    }
}

