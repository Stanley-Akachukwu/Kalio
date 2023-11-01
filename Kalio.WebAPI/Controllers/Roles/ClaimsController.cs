using Kalio.Common;
using Kalio.Domain.Roles;
using Kalio.Domain.Roles.Claims;
using Kalio.WebAPI.Config;
using Kalio.WebAPI.Securities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using System.Security.Claims;

namespace Kalio.WebAPI.Controllers.Roles
{
    [ApiController]
    [Route("[controller]")]
    public class ClaimsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClaimsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [EnableQuery]
        [HttpGet]
        [KalioAuthorize(Permissions.RetrieveClaimsAccess)]
        [ProducesResponseType(typeof(ODataResponse<ClaimViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            var request = new QueryClaimCommand();

            var rsp = await _mediator.Send(request);
            var result = await ControllerUtil.MapResponseByStatusCode(rsp.Response, rsp.StatusCode);
            return result;
        }
        [HttpPost("create")]
        [KalioAuthorize(Permissions.CreateClaimAccess)]
        [ProducesResponseType(typeof(CommandResult<ClaimViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateClaimCommand model)
        {

            var rsp = await _mediator.Send(model);
            var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
            return result;
        }

        [HttpPost("update")]
        [KalioAuthorize(Permissions.UpdateClaimAccess)]
        [ProducesResponseType(typeof(CommandResult<ClaimViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] UpdateClaimCommand model)
        {

            var rsp = await _mediator.Send(model);
            var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
            return result;
        }

        [HttpPost("delete")]
        [KalioAuthorize(Permissions.DeleteClaimAccess)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromBody] DeleteRoleCommand model)
        {
            var rsp = await _mediator.Send(model);
            var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
            return result;
        }
 
    }
}


