﻿using EventBackend.Services.Interfaces;
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

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        // GET: api/Events
        [HttpGet]
        public async Task<IActionResult> GetEvents()
        {
            return Ok(await _eventService.GetAllEventsAsync());
        }

        // GET: api/Events/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvent([FromRoute][Required]Guid id)
        {
            var entity = await _eventService.GetEventByIdAsync(id);

            return Ok(entity);
        }

        // POST: api/Events
        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody]Event entity)
        {
            await _eventService.CreateEventAsync(entity);

            //    return CreatedAtAction("PostTodoItem", new { id = todoItem.Id }, todoItem);
            return CreatedAtAction(
                nameof(CreateEvent),
                new { entity.Title, entity.StartDate, entity.EndDate, entity.Duration}, 
                entity
                );
        }

        // PUT: api/Events/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent([FromRoute][Required]Guid id, [FromBody]Event entity)
        {
            return Ok(await _eventService.UpdateEventAsync(id, entity));
        }

        // DELETE: api/Events/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent([FromRoute][Required]Guid id)
        {
            await _eventService.DeleteEventAsync(id);

            return NoContent();
        }
    }
}

