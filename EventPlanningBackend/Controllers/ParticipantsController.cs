using EventBackend.Entities;
using EventBackend.Filters;
using EventBackend.Models.Requests;
using EventBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EventBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantsController : ControllerBase
    {

        private readonly IParticipantsService _participantService;

        public ParticipantsController(IParticipantsService participantService)
        {
            _participantService = participantService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllParticipantsAsync([FromQuery] ParticipantQuery filter)
        {
            return Ok(await _participantService.GetAllParticipantsAsync(filter));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetParticipantById([Required][FromRoute] Guid id)
        {
            return Ok(await _participantService.GetParticipantByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateParticipant([FromBody] ParticipantRequest request)
        {
            var participant = new Participant
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                BirthDate = request.BirthDate,
                Email = request.Email
            };

            var result = await _participantService.CreateParticipantAsync(participant);

            return CreatedAtAction(nameof(CreateParticipant), participant);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateParticipant([Required][FromRoute] Guid id, [FromBody] ParticipantRequest request)
        {
            var participant = new Participant
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                BirthDate = request.BirthDate,
                Email = request.Email
            };

            return Ok(await _participantService.UpdateParticipantAsync(id, participant));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParticipantAsync([Required][FromRoute] Guid id)
        {
            await _participantService.DeleteParticipantAsync(id);

            return NoContent();
        }
    }
}
