using EventBackend.Models.Requests;
using EventBackend.Services.Interfaces;
using EventDomain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;


namespace EventPlanningBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;
        //private readonly IEventTaskService _taskService;

        public EventController(
            IEventService eventService
            //IEventTaskService taskService
            )
        {
            _eventService = eventService;
            //_taskService = taskService;
        }

        // GET: api/Events
        [HttpGet]
        public async Task<IActionResult> GetEvents()
        {
            return Ok(await _eventService.GetAllEventsAsync());
        }

        // GET: api/Events/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvent([FromRoute][Required] Guid id)
        {
            var entity = await _eventService.GetEventByIdAsync(id);

            return Ok(entity);
        }

        // POST: api/Events
        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] EventRequest request)
        {
            //var convertedEntity = new Event
            //{
            //    Title = entity.Title,
            //    StartDate = entity.StartDate,
            //    EndDate = entity.EndDate,
            //    Description = entity.Description
            //};
            var response = await _eventService.CreateEventAsync(request);


            //    return CreatedAtAction("PostTodoItem", new { id = todoItem.Id }, todoItem);
            return CreatedAtAction(
                nameof(CreateEvent), response);
        }

        // PUT: api/Events/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent([FromRoute][Required] Guid id, [FromBody] EventRequest entity)
        {
            return Ok(await _eventService.UpdateEventAsync(id, entity));
        }

        // DELETE: api/Events/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent([FromRoute][Required] Guid id)
        {
            await _eventService.DeleteEventAsync(id);

            return NoContent();
        }

        //[HttpGet]
        //[Route("/{id}/Tasks")]
        //public async Task<IActionResult> GetEventTasks([FromRoute][Required] Guid EventId)
        //{
        //    var tasks = await _taskService.GetAllTasksAsync();
        //}

    }
}

