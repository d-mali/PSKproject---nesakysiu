using EventBackend.Entities;
using EventBackend.Filters;
using EventBackend.Services.Interfaces;
using EventDomain.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;


namespace EventBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController(
        IEventsService eventService)
        : ControllerBase
    {
        // GET: api/Events
        [HttpGet]
        public async Task<IActionResult> GetEvents([FromQuery] EventsQuery filter)
        {
            var user = User;
            return Ok(await eventService.GetAllEventsAsync(filter));
        }

        // GET: api/Events/5
        [HttpGet("{eventId}")]
        public async Task<IActionResult> GetEvent([FromRoute][Required] Guid eventId)
        {
            var entity = await eventService.GetEventAsync(eventId);

            return Ok(entity);
        }

        // POST: api/Events
        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] EventRequest request)
        {
            var response = await eventService.CreateEventAsync(request);

            //    return CreatedAtAction("PostTodoItem", new { eventId = todoItem.eventId }, todoItem);
            return CreatedAtAction(
                nameof(CreateEvent), response);
        }

        // PUT: api/Events/5
        [HttpPut("{eventId}")]
        public async Task<IActionResult> UpdateEvent(Guid eventId, [FromBody] EventRequest entity)
        {
            return Ok(await eventService.UpdateEventAsync(eventId, entity));
        }

        // DELETE: api/Events/5
        [HttpDelete("{eventId}")]
        public async Task<IActionResult> DeleteEvent([FromRoute][Required] Guid eventId)
        {
            await eventService.DeleteEventAsync(eventId);

            return NoContent();
        }

        [HttpGet("{eventId}/Participation")]
        public async Task<IActionResult> GetEventParticipants([FromRoute][Required] Guid eventId)
        {
            var participantsList = await eventService.GetEventParticipants(eventId);
            if (participantsList == null)
            {
                return BadRequest("Invalid");
            }
            var participants = participantsList.Select(c => new Participant
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                BirthDate = c.BirthDate,
                Email = c.Email,
            }).ToList();
            return Ok(participants);
        }


        [HttpGet("{eventId}/Participation/{participantId}")]
        public async Task<IActionResult> GetEventParticipant([FromRoute][Required] Guid eventId, Guid participantId)
        {
            var participant = await eventService.GetEventParticipants(eventId);
            if (participant == null)
            {
                return BadRequest("Invalid");
            }

            var participantResponse = participant.Single().ToResponse();
            return Ok(participantResponse);
        }


        [HttpPut("{eventId}/Participation/{participantId}")]
        public async Task<IActionResult> AddEventParticipant([FromRoute][Required] Guid eventId, Guid participantId)
        {
            var eventResponse = await eventService.CreateParticipation(eventId, participantId);
            if (eventResponse == null)
            {
                return BadRequest("Invalid");
            }
            return Ok(eventResponse);
        }

        [HttpDelete("{eventId}/Participation/{participantId}")]
        public async Task<IActionResult> DeleteEventParticipant([FromRoute][Required] Guid eventId, Guid participantId)
        {
            var eventResponse = await eventService.DeleteParticipation(eventId, participantId);
            if (eventResponse == null)
            {
                return BadRequest("Invalid");
            }
            return Ok(eventResponse);
        }

        [HttpPut("{eventId}/Workers/{userId}")]
        public async Task<IActionResult> GetEventWorker([FromRoute][Required] Guid eventId, String userId)
        {
            var eventResponse = await eventService.CreateWorker(eventId, userId);
            if (eventResponse == null)
            {
                return BadRequest("Invalid");
            }
            return Ok(eventResponse);
        }

        [HttpGet("{eventId}/Workers")]
        public async Task<IActionResult> GetEventWorkers([FromRoute][Required] Guid eventId)
        {
            var eventWorkers = await eventService.GetEventWorkers(eventId);
            if (eventWorkers == null)
            {
                return BadRequest("Invalid");
            }
            var workers = eventWorkers.Select(c => new ApplicationUser
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
            }).ToList();
            return Ok(workers);
        }

        [HttpDelete("{eventId}/Workers/{userId}")]
        public async Task<IActionResult> DeleteEventParticipant([FromRoute][Required] Guid eventId, string userId)
        {
            var eventResponse = await eventService.DeleteWorker(eventId, userId);
            if (eventResponse == null)
            {
                return BadRequest("Invalid");
            }
            return Ok(eventResponse);
        }
    }
}

