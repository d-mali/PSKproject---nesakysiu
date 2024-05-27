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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvent([FromRoute][Required] Guid id)
        {
            var entity = await _eventService.GetEventAsync(id);

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

        //[HttpPost]
        //public async Task<IActionResult> CreateParticipation([FromBody] ParticipationRequest request)
        //{
        //    var eventas = await _eventService.CreateParticipation(request);
        //
        //    if (eventas == null)
        //    {
        //        return BadRequest("Invalid event ID or participant ID");
        //    }
        //    return Ok("Participation created successfully");
        //}
        [HttpGet("{id}/Participation")]
        public async Task<IActionResult> GetEventParticipantss([FromRoute][Required] Guid id)
        {
            var eventas = await _eventService.GetEventParticipants(id);
            if (eventas == null)
            {
                return BadRequest("Invalid");
            }
            return Ok(eventas);
        }


        [HttpGet("{id}/Participation/{participantId}")]
        public async Task<IActionResult> GetEventParticipantss([FromRoute][Required] Guid id, Guid participantId)
        {
            var eventas = await _eventService.GetEventParticipants(id);
            if (eventas == null)
            {
                return BadRequest("Invalid");
            }
            return Ok(eventas);
        }


        [HttpPut("{id}/Participation/{participantId}")]
        public async Task<IActionResult> GetEventParticipants([FromRoute][Required] Guid id, Guid participantId)
        {
            var eventas = await _eventService.GetEventParticipants(id);
            if (eventas == null)
            {
                return BadRequest("Invalid");
            }
            return Ok(eventas);
        }

        [HttpDelete("{id}/Participation/{participantId}")]
        public Task<IActionResult> DeleteEventParticipant([FromRoute][Required] Guid id, Guid participantId)
        {
            //var eventas = await _eventService.GetEventParticipants(id);
            //if (eventas == null)
            //{
            //    return BadRequest("Invalid");
            //}
            //return Ok(eventas);
            throw new NotImplementedException();
        }

        //[HttpGet]
        //[Route("/{id}/Tasks")]
        //public async Task<IActionResult> GetEventTasks([FromRoute][Required] Guid eventId)
        //{
        //    //var tasks = await _taskService.GetEventTasksAsync(eventId);
        //
        //
        //    //return Ok(tasks);
        //}

        //[HttpGet]
        //[Route("/{id}/User")]
        //public async Task<IActionResult> GetEventUsers([FromQuery] UserQuery query)
        //{
        //    throw new NotImplementedException();
        //}

        public class ParticipationRequest
        {
            public Guid EventId { get; set; }
            public Guid ParticipantId { get; set; }
        }

        [HttpGet("{id}/Tasks")]
        public async Task<IActionResult> GetTasks(Guid id)
        {
            return Ok(await _taskService.GetTasksAsync());
        }

        // GET: api/Tasks/5
        [HttpGet("{id}/Tasks/{taskId}")]
        public async Task<IActionResult> GetTask(Guid id, Guid taskId)
        {
            var task = await _taskService.GetTaskAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        [HttpPost("{id}/Tasks")]
        public async Task<IActionResult> CreateTask(Guid id, TaskRequest taskRequest)
        {
            var createdTask = await _taskService.CreateTaskAsync(taskRequest);

            //return CreatedAtAction(nameof(CreateTask),
            //    createdTask
            //    );
            return Ok(createdTask);
        }

        // PUT: api/Tasks/5
        [HttpPut("{id}/Tasks/{taskId}")]
        public async Task<IActionResult> UpdateTask(Guid id, Guid taskId, TaskRequest taskRequest)
        {
            var updatedTask = await _taskService.UpdateTaskAsync(id, taskRequest);

            return Ok(updatedTask);
        }

        // DELETE: api/Tasks/5
        [HttpDelete("{id}/Tasks/{taskId}")]
        public async Task<IActionResult> DeleteTask(Guid id, Guid taskId)
        {
            await _taskService.DeleteTaskAsync(id);

            return NoContent();
        }


    }
}

