using EventBackend.Services.Interfaces;
using EventDomain.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EventBackend.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUsersService _userService;

        public UsersController(IUsersService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersAsync()
        {
            return Ok(await _userService.GetAllUsersAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByIdAsync([FromRoute][Required] string id)
        {
            return Ok(await _userService.GetUserByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] EmployeeRequest request)
        {
            var user = new EmployeeRequest
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
            };

            var result = await _userService.CreateUserAsync(user);

            return CreatedAtAction(nameof(CreateUser), user);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateParticipant([Required][FromRoute] string userId, [FromBody] EmployeeRequest request)
        {
            var participant = new EmployeeRequest
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
            };

            return Ok(await _userService.UpdateUserAsync(userId, participant));
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteParticipantAsync([Required][FromRoute] String userId)
        {
            await _userService.DeleteUserAsync(userId);

            return NoContent();
        }
    }
}
