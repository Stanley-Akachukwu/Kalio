using Kalio.Common;
using Kalio.Domain.Roles;
using Kalio.Domain.Users;
using Kalio.WebAPI.Config;
using Kalio.WebAPI.Securities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using System.Security.Claims;

namespace Kalio.WebAPI.Controllers.Users
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [EnableQuery]
        [HttpGet]
        [KalioAuthorize(Permissions.RetrieveUserAccess)]
        [ProducesResponseType(typeof(ODataResponse<UserViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            var request = new QueryUserCommand();

            var rsp = await _mediator.Send(request);
            var result = await ControllerUtil.MapResponseByStatusCode(rsp.Response, rsp.StatusCode);
            return result;
        }

        [HttpPost("create")]
        [KalioAuthorize(Permissions.CreateUserAccess)]
        [ProducesResponseType(typeof(CommandResult<UserViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand model)
        {

            var rsp = await _mediator.Send(model);
            var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
            return result;
        }

        [HttpPost("update")]
        [KalioAuthorize(Permissions.UpdateUserAccess)]
        [ProducesResponseType(typeof(CommandResult<RoleViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] UpdateUserCommand model)
        {
            var rsp = await _mediator.Send(model);
            var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
            return result;
        }

        [HttpPost("delete")]
        [KalioAuthorize(Permissions.DeleteUserAccess)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> delete([FromBody] UpdateUserCommand model)
        {
            var rsp = await _mediator.Send(model);
            var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
            return result;
        }
        [HttpPost("forgetPassword")]
        [KalioAuthorize(Permissions.InitiateForgetPasswordAccess)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordCommand model)
        {
            var rsp = await _mediator.Send(model);
            var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
            return result;
        }
        [HttpPost("resetPassword")]
        [KalioAuthorize(Permissions.ResetPasswordAccess)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand model)
        {
            var rsp = await _mediator.Send(model);
            var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
            return result;
        }
        [HttpPost("addUserInRole")]
        [KalioAuthorize(Permissions.AddUserInRoleAccess)]
        [ProducesResponseType(typeof(CommandResult<RoleViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddUserInRole([FromBody] AddUserInRoleCommand model)
        {
            var rsp = await _mediator.Send(model);
            var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
            return result;
        }
        [EnableQuery]
        [HttpPost("getUserClaims")]
        [KalioAuthorize(Permissions.GetUserClaimAccess)]
        [ProducesResponseType(typeof(CommandResult<List<string>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUserClaims([FromBody] GetUserClaimsCommand model)
        {
            var rsp = await _mediator.Send(model);
            var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
            return result;
        }
        [HttpPost("updateClaimsForUser")]
        [KalioAuthorize(Permissions.UpdateUserClaimAccess)]
        [ProducesResponseType(typeof(CommandResult<IQueryable<Claim>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommandResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateClaims([FromBody] UpdateClaimsForUserCommand model)
        {
            var rsp = await _mediator.Send(model);
            var result = await ControllerUtil.MapResponseByStatusCode(rsp, rsp.StatusCode);
            return result;
        }
    }
}
