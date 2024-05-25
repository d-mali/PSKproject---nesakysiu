using EventBackend.Filters;
using EventBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EventBackend.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersAsync(UserQuery filter)
        {
            return Ok(await _userService.GetAllUsersAsync(filter));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByIdAsync([FromRoute][Required] Guid id)
        {
            return Ok(await _userService.GetUserByIdAsync(id));
        }
    }
}
