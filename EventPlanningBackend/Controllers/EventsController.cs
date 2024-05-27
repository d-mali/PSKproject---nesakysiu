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
        private readonly ITasksService _taskService;

        public EventsController(
            IEventsService eventService,
            ITasksService taskService
            )
        {
            _eventService = eventService;
            _taskService = taskService;

        }

        // GET: api/Events
        [HttpGet]
        public async Task<IActionResult> GetEvents([FromQuery] EventsQuery filter)
        {
            var user = User;
            return Ok(await _eventService.GetAllEventsAsync(filter));
        }

        // GET: api/Events/5
        [HttpGet("{eventId}")]
        public async Task<IActionResult> GetEvent([FromRoute][Required] Guid eventId)
        {
            var entity = await _eventService.GetEventAsync(eventId);

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


            //    return CreatedAtAction("PostTodoItem", new { eventId = todoItem.eventId }, todoItem);
            return CreatedAtAction(
                nameof(CreateEvent), response);
        }

        // PUT: api/Events/5
        [HttpPut("{eventId}")]
        public async Task<IActionResult> UpdateEvent(Guid eventId, [FromBody] EventRequest entity)
        {
            return Ok(await _eventService.UpdateEventAsync(eventId, entity));
        }

        // DELETE: api/Events/5
        [HttpDelete("{eventId}")]
        public async Task<IActionResult> DeleteEvent([FromRoute][Required] Guid eventId)
        {
            await _eventService.DeleteEventAsync(eventId);

            return NoContent();
        }

        //[HttpPost]
        //public async Task<IActionResult> CreateParticipation([FromBody] ParticipationRequest request)
        //{
        //    var eventas = await _eventService.CreateParticipation(request);
        //
        //    if (eventas == null)
        //    {
        //        return BadRequest("Invalid event eventId or participant eventId");
        //    }
        //    return Ok("Participation created successfully");
        //}
        [HttpGet("{eventId}/Participation")]
        public async Task<IActionResult> GetEventParticipantss([FromRoute][Required] Guid eventId)
        {
            var eventas = await _eventService.GetEventParticipants(eventId);
            if (eventas == null)
            {
                return BadRequest("Invalid");
            }
            return Ok(eventas);
        }


        [HttpGet("{eventId}/Participation/{participantId}")]
        public async Task<IActionResult> GetEventParticipantss([FromRoute][Required] Guid eventId, Guid participantId)
        {
            var eventas = await _eventService.GetEventParticipants(eventId);
            if (eventas == null)
            {
                return BadRequest("Invalid");
            }
            return Ok(eventas);
        }


        [HttpPut("{eventId}/Participation/{participantId}")]
        public async Task<IActionResult> GetEventParticipants([FromRoute][Required] Guid eventId, Guid participantId)
        {
            var eventas = await _eventService.GetEventParticipants(eventId);
            if (eventas == null)
            {
                return BadRequest("Invalid");
            }
            return Ok(eventas);
        }

        [HttpDelete("{eventId}/Participation/{participantId}")]
        public Task<IActionResult> DeleteEventParticipant([FromRoute][Required] Guid eventId, Guid participantId)
        {
            //var eventas = await _eventService.GetEventParticipants(eventId);
            //if (eventas == null)
            //{
            //    return BadRequest("Invalid");
            //}
            //return Ok(eventas);
            throw new NotImplementedException();
        }

        //[HttpGet]
        //[Route("/{eventId}/Tasks")]
        //public async Task<IActionResult> GetEventTasks([FromRoute][Required] Guid eventId)
        //{
        //    //var tasks = await _taskService.GetEventTasksAsync(eventId);
        //
        //
        //    //return Ok(tasks);
        //}

        //[HttpGet]
        //[Route("/{eventId}/User")]
        //public async Task<IActionResult> GetEventUsers([FromQuery] UserQuery query)
        //{
        //    throw new NotImplementedException();
        //}

        public class ParticipationRequest
        {
            public Guid EventId { get; set; }
            public Guid ParticipantId { get; set; }
        }

        //[HttpGet("{eventId}/Tasks")]
        //public async Task<IActionResult> GetTasks(Guid eventId)
        //{
        //    return Ok(await _taskService.GetTasksAsync());
        //}
        //
        //// GET: api/Tasks/5
        //[HttpGet("{eventId}/Tasks/{taskId}")]
        //public async Task<IActionResult> GetTask(Guid eventId, Guid taskId)
        //{
        //    var task = await _taskService.GetTaskAsync(eventId);
        //
        //    if (task == null)
        //    {
        //        return NotFound();
        //    }
        //
        //    return Ok(task);
        //}
        //
        //[HttpPost("{eventId}/Tasks")]
        //public async Task<IActionResult> CreateTask(Guid eventId, TaskRequest taskRequest)
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
        //[HttpPut("{eventId}/Tasks/{taskId}")]
        //public async Task<IActionResult> UpdateTask(Guid eventId, Guid taskId, TaskRequest taskRequest)
        //{
        //    var updatedTask = await _taskService.UpdateTaskAsync(eventId, taskRequest);
        //
        //    return Ok(updatedTask);
        //}
        //
        //// DELETE: api/Tasks/5
        //[HttpDelete("{eventId}/Tasks/{taskId}")]
        //public async Task<IActionResult> DeleteTask(Guid eventId, Guid taskId)
        //{
        //    await _taskService.DeleteTaskAsync(eventId);
        //
        //    return NoContent();
        //}


    }
}

