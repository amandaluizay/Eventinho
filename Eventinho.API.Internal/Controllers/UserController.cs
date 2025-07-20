using Eventinho.Shared.Interfaces;
using EventinhoApplication.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Eventinho.API.Internal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService testeUserService)
        {
            _userService = testeUserService;
        }

        [HttpPost("CreateAccount")]
        public async Task<IActionResult> CreateAccount([FromBody] UserRequest userRequest)
        {
            var token = await _userService.CreateUserAsync(userRequest.UserName, userRequest.Email, userRequest.PasswordHash);

            return Ok(token);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserRequest userRequest)
        {
            var token = await _userService.LoginUserAsync(userRequest.Email, userRequest.PasswordHash);
            return Ok(token);
        }
    }
}
