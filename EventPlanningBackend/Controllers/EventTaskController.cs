using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using EventDataAccess.Repositories;
using EventDomain.Entities;
using EventBackend.Services.Interfaces;
using EventBackend.Models.Requests;


namespace EventPlanningBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventTaskController : ControllerBase
    {
        private readonly IEventTaskService _taskService;

        public EventTaskController(IEventTaskService taskService)
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
        public async Task<IActionResult> CreateTask(EventTaskRequest request)
        {
            await _taskService.CreateTaskAsync(request);

            return CreatedAtAction(nameof(CreateTask),
                new { request.Title, request.ScheduledTime, request.Description },
                request
                );
        }

        // PUT: api/Tasks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(Guid id, EventTaskRequest entity)
        {
            return Ok(await _taskService.UpdateTaskAsync(id, entity));
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

