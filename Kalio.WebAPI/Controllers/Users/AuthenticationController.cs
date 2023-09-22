using Kalio.Common;
using Kalio.Core.Services.Users;
using Kalio.Domain.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kalio.WebAPI.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IUserRepository _userRepository;
        public AuthenticationController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpPost]
        [ProducesResponseType(typeof(CommandResult<AuthenticatedResult>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Authenticate([FromBody] LoginViewModel model)
        {
            //Change to mediator
            var rsp = await _userRepository.AuthenticateUserAsync(model);
            return Ok(rsp);
        }

    }
}
