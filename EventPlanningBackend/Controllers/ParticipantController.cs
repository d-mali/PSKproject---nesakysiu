using EventBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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

        //GetAllParticipants
        //GetParticipantById
        //CreateParticipant
        //UpdateParticipant
        //DeleteParticipant
    }
}
