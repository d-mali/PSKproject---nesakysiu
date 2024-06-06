using EventBackend.Entities;
using EventBackend.Services.Interfaces;
using EventDomain.Contracts.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace EventBackend.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IUsersService userService) : ControllerBase
    {
        [HttpGet]
        [Route("/api/User")]
        [Authorize]
        public async Task<IActionResult> GetUserAsync()

        {
            var identity = User.FindFirst(ClaimTypes.NameIdentifier);

            if (identity == null)
            {
                return BadRequest("Invalid");
            }

            var id = identity.Value;

            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Invalid");
            }

            return Ok(await userService.GetUserByIdAsync(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersAsync()
        {
            return Ok(await userService.GetAllUsersAsync());
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserByIdAsync([FromRoute][Required] string userId)
        {
            return Ok(await userService.GetUserByIdAsync(userId));
        }

        [HttpGet("{userId}/Tasks")]
        public async Task<IActionResult> GetUserTasks([FromRoute][Required] string userId)
        {
            return Ok(await userService.GetUserTasks(userId));
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] EmployeeRequest request)
        {
            var user = new EmployeeRequest
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
            };

            var result = await userService.CreateUserAsync(user);

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

            return Ok(await userService.UpdateUserAsync(userId, participant));
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteParticipantAsync([Required][FromRoute] string userId)
        {
            await userService.DeleteUserAsync(userId);

            return NoContent();
        }

        [HttpPut("{userId}/Tasks/{taskId}")]
        public async Task<IActionResult> GetEventWorker([FromRoute][Required] string userId, Guid taskId)
        {
            var task = await userService.CreateTasking(userId, taskId);
            if (task == null)
            {
                return BadRequest("Invalid");
            }

            return Ok(task);
        }

        [HttpGet("{userId}/Events/{eventId}/Tasks")]
        public async Task<IActionResult> GetEventWorkers([FromRoute][Required] string userId, Guid eventId)
        {
            var userTasks = await userService.GetUserTasks(userId, eventId);

            var workers = userTasks.Select(c => new EventTask
            {
                Id = c.Id,
                Title = c.Title,
                ScheduledTime = c.ScheduledTime,
                Description = c.Description,
                Status = c.Status
            }).ToList();

            return Ok(workers);
        }

        [HttpDelete("{userId}/Tasks/{taskId}")]
        public async Task<IActionResult> DeleteEventParticipant([FromRoute][Required] string userId, Guid taskId)
        {
            var user = await userService.DeleteTasking(userId, taskId);
            if (user == null)
            {
                return BadRequest("Invalid");
            }
            return Ok(user);
        }
    }
}
