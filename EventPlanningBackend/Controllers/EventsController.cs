using EventBackend.Filters;
using EventBackend.Services.Interfaces;
<<<<<<< HEAD
using EventDomain.Entities;
=======
using EventDomain.Contracts.Requests;
>>>>>>> d4ba34c0afd4c00ce362efca4531de031b30db4f
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace EventBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventsService _eventService;
<<<<<<< HEAD
        private readonly ITasksService _taskService;
        protected readonly MainDbContext _context;

        public EventsController(
            IEventsService eventService,
            ITasksService taskService,
            MainDbContext context
            )
        {
            _eventService = eventService;
            _taskService = taskService;
            _context = context;
=======

        public EventsController(
            IEventsService eventService
            )
        {
            _eventService = eventService;
>>>>>>> d4ba34c0afd4c00ce362efca4531de031b30db4f
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


            var eventas = await _context.Events.FindAsync(request.EventId);
            var participant = await _context.Participants.FindAsync(request.ParticipantId);

            if (eventas == null || participant == null)
            {
                return BadRequest("Invalid event ID or participant ID");
            }
            eventas.Participants ??= new List<Participant>();

            eventas.Participants.Add(participant);
            _context.SaveChanges();

            return Ok("Participation created successfully");
        }

        [HttpGet]
        [Route("/{id}/Participants")]
        public async Task<IActionResult> GetEventParticipants([FromRoute][Required] Guid id)
        {
            var eventWithParticipants = await _context.Events
                .Include(s => s.Participants)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (eventWithParticipants == null)
            {
                return BadRequest("Invalid participant ID");
            }
            if (eventWithParticipants.Participants == null)
            {
                return BadRequest("No participants");
            }
            var participants = eventWithParticipants.Participants.Select(c => new Participant
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                BirthDate = c.BirthDate,
                Email = c.Email,
            }).ToList();
            return Ok(participants);
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

