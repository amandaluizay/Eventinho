
using Microsoft.AspNetCore.Mvc;
using TesteJWT.Interfaces;
using TesteJWT.Models;

namespace TesteJWT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TesteUserController : ControllerBase
    {
        private readonly ITesteUserService _testeUserService;
        public TesteUserController(ITesteUserService testeUserService)
        {
            _testeUserService = testeUserService;
        }

        [HttpPost("CreateAccount")]
        public async Task<IActionResult> CreateAccount([FromBody] UserRequest userRequest)
        {
            var token = await _testeUserService.CreateUserAsync(userRequest.UserName, userRequest.Email, userRequest.PasswordHash);

            return Ok(token);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserRequest userRequest)
        {
            var token = await _testeUserService.LoginUserAsync(userRequest.Email, userRequest.PasswordHash);
            return Ok(token);
        }
    }
}
