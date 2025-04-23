using Eventinho.Domain.Entities;
using Eventinho.Shared.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Eventinho.API.Internal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController(ITokenService tokenService) : ControllerBase
    {
        private readonly ITokenService _tokenService = tokenService;

        [Authorize]
        // POST api/<ValuesController>
        [HttpPost("sign_in")]
        public IActionResult Post([FromBody] string username)
        {
            return Ok( username);
        }

        [HttpPost("login")]
        public IActionResult Post([FromBody] string email, string password)
        {
            var user = new User()
            {
                UserName = email,
                Email = email,
                PasswordHash = password
            };

            var token = _tokenService.Generate(user);

            return Ok(token);
        }
    }
}
