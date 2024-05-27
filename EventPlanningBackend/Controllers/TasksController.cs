using EventBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace EventBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITasksService _taskService;

        public TasksController(ITasksService taskService)
        {
            _taskService = taskService;
        }

        // GET: api/Tasks
        //[HttpGet]
        //public async Task<IActionResult> GetTasks()
        //{
        //    return Ok(await _taskService.GetTasksAsync());
        //}
        //
        //// GET: api/Tasks/5
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetTask(Guid id)
        //{
        //    var task = await _taskService.GetTaskAsync(id);
        //
        //    if (task == null)
        //    {
        //        return NotFound();
        //    }
        //
        //    return Ok(task);
        //}
        //
        //[HttpPost]
        //public async Task<IActionResult> CreateTask(TaskRequest taskRequest)
        //{
        //    var createdTask = await _taskService.CreateTaskAsync(taskRequest);
        //
        //    //return CreatedAtAction(nameof(CreateTask),
        //    //    createdTask
        //    //    );
        //    return Ok(createdTask);
        //}
        //
        //// PUT: api/Tasks/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateTask(Guid id, TaskRequest taskRequest)
        //{
        //    var updatedTask = await _taskService.UpdateTaskAsync(id, taskRequest);
        //
        //    return Ok(updatedTask);
        //}
        //
        //// DELETE: api/Tasks/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTask(Guid id)
        //{
        //    await _taskService.DeleteTaskAsync(id);
        //
        //    return NoContent();
        //}


    }
}

