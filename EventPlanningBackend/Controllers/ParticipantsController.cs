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

        [HttpGet("{participantId}")]
        public async Task<IActionResult> GetParticipantById([Required][FromRoute] Guid participantId)
        {
            return Ok(await _participantService.GetParticipantByIdAsync(participantId));
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

        [HttpPut("{participantId}")]
        public async Task<IActionResult> UpdateParticipant([Required][FromRoute] Guid participantId, [FromBody] ParticipantRequest request)
        {
            var participant = new Participant
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                BirthDate = request.BirthDate,
                Email = request.Email
            };

            return Ok(await _participantService.UpdateParticipantAsync(participantId, participant));
        }

        [HttpDelete("{participantId}")]
        public async Task<IActionResult> DeleteParticipantAsync([Required][FromRoute] Guid participantId)
        {
            await _participantService.DeleteParticipantAsync(participantId);

            return NoContent();
        }
    }
}
