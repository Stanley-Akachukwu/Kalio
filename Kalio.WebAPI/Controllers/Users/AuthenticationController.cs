using AutoMapper;
using Kalio.Common;
using Kalio.Core.Users;
using Kalio.Domain.Users;
using Kalio.Entities;
using Kalio.WebAPI.Config;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Kalio.WebAPI.Controllers.Users
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly KalioIdentityDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private IConfiguration _configuration;
        private readonly IMediator _mediator;
        public AuthenticationController(KalioIdentityDbContext dbContext, IMapper mapper, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, IConfiguration configuration, IMediator mediator)
        {

            _dbContext = dbContext;
            _mapper = mapper;
            _roleManager = roleManager;
            _userManager = userManager;
            _configuration = configuration;
            _mediator = mediator;
        }
        
       
        [HttpPost("login")]
        [ProducesResponseType(typeof(CommandResult<AuthenticatedResult>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateUserCommand model)
        {
            var rsp = await _mediator.Send(model);
            var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
            return result;
        }
    }
}
