using EventBackend.Filters;
using EventBackend.Services.Interfaces;
using EventDomain.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;


namespace EventBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventsService _eventService;


        public EventsController(
            IEventsService eventService
            )
        {
            _eventService = eventService;

        }

        // GET: api/Events
        [HttpGet]
        public async Task<IActionResult> GetEvents([FromQuery] EventsQuery filter)
        {
            var user = User;
            return Ok(await _eventService.GetAllEventsAsync(filter));
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
        public async Task<IActionResult> UpdateEvent(Guid id, [FromBody] EventRequest entity)
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

        [HttpPost]
        [Route("/CreateParticipation")]
        public async Task<IActionResult> CreateParticipation([FromBody] ParticipationRequest request)
        {
            var eventas = await _eventService.CreateParticipation(request);

            if (eventas == null)
            {
                return BadRequest("Invalid event ID or participant ID");
            }
            return Ok("Participation created successfully");
        }

        [HttpGet]
        [Route("/{id}/Participants")]
        public async Task<IActionResult> GetEventParticipants([FromRoute][Required] Guid id)
        {
            var eventas = await _eventService.GetEventParticipants(id);
            if (eventas == null)
            {
                return BadRequest("Invalid");
            }
            return Ok(eventas);
        }

        //[HttpGet]
        //[Route("/{id}/Tasks")]
        //public async Task<IActionResult> GetEventTasks([FromRoute][Required] Guid eventId)
        //{
        //    //var tasks = await _taskService.GetEventTasksAsync(eventId);
        //
        //    //return Ok(tasks);
        //}

        //[HttpGet]
        //[Route("/{id}/User")]
        //public async Task<IActionResult> GetEventUsers([FromQuery] UserQuery query)
        //{

        //}
        public class ParticipationRequest
        {
            public Guid EventId { get; set; }
            public Guid ParticipantId { get; set; }
        }

    }
}

