using EventBackend.Models.Requests;
using EventBackend.Services.Interfaces;
using EventDomain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EventBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantController : ControllerBase
    {

        private readonly IParticipantService _participantService;

        public ParticipantController(IParticipantService participantService)
        {
            _participantService = participantService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllParticipantsAsync()
        {
            return Ok(await _participantService.GetAllParticipantsAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetParticipantById([Required][FromRoute]Guid id)
        {
            return Ok(await _participantService.GetParticipantByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateParticipant([FromBody]ParticipantRequest request)
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
        public async Task<IActionResult> UpdateParticipant([Required][FromRoute]Guid id, [FromBody]ParticipantRequest request)
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
        public async Task<IActionResult> DeleteParticipantAsync([Required][FromRoute]Guid id)
        {
            await _participantService.DeleteParticipantAsync(id);

            return NoContent();
        }
    }
}
