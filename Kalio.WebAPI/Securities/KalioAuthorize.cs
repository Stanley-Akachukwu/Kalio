using Kalio.Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

 namespace Kalio.WebAPI.Securities
{
    public class KalioAuthorize : Attribute, IAuthorizationFilter
    {
        readonly string _claim;
        public KalioAuthorize(string claim)
        {
            _claim = claim;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (!user.Identity.IsAuthenticated)
            {
                context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Unauthorized);
                return;
            }

            var _mediator = context.HttpContext.RequestServices.GetService<IMediator>();

            var userId = user.Claims.FirstOrDefault(c => c.Type == "Email").Value;
            try
            {
                var result = _mediator.Send(new GetUserClaimsCommand() { UserId = userId }).Result;
                var claims = result.Response;
                if (!claims.Contains(_claim))
                {
                    context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Forbidden);
                    return;
                }
            }
            catch (Exception)
            {
                context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Forbidden);
                return;
            }
        }
    }
}
