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
    public class ParticipantsController(IParticipantsService participantService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllParticipantsAsync([FromQuery] ParticipantQuery filter)
        {
            return Ok(await participantService.GetAllParticipantsAsync(filter));
        }

        [HttpGet("{participantId}")]
        public async Task<IActionResult> GetParticipantById([Required][FromRoute] Guid participantId)
        {
            return Ok(await participantService.GetParticipantByIdAsync(participantId));
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

            var result = await participantService.CreateParticipantAsync(participant);

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

            return Ok(await participantService.UpdateParticipantAsync(participantId, participant));
        }

        [HttpDelete("{participantId}")]
        public async Task<IActionResult> DeleteParticipantAsync([Required][FromRoute] Guid participantId)
        {
            await participantService.DeleteParticipantAsync(participantId);

            return NoContent();
        }
    }
}
