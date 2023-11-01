using Kalio.Common;
using Kalio.Domain.Roles;
using Kalio.Domain.Users;
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
    public class RolesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RolesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [EnableQuery]
        [HttpGet]
        [KalioAuthorize(Permissions.RetrieveRolesAccess)]
        [ProducesResponseType(typeof(ODataResponse<RoleViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            var request = new QueryRoleCommand();

            var rsp = await _mediator.Send(request);
            var result = await ControllerUtil.MapResponseByStatusCode(rsp.Response, rsp.StatusCode);
            return result;
        }
        [HttpPost("create")]
        [KalioAuthorize(Permissions.CreateRoleAccess)]
        [ProducesResponseType(typeof(CommandResult<RoleViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateRoleCommand model)
        {

            var rsp = await _mediator.Send(model);
            var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
            return result;
        }

        [HttpPost("update")]
        [KalioAuthorize(Permissions.UpdateRoleAccess)]
        [ProducesResponseType(typeof(CommandResult<RoleViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] UpdateRoleCommand model)
        {

            var rsp = await _mediator.Send(model);
            var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
            return result;
        }

        [HttpPost("delete")]
        [KalioAuthorize(Permissions.DeleteRoleAccess)]
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


