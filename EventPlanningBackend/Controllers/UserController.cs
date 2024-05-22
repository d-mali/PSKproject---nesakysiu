using EventBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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

    }
}
