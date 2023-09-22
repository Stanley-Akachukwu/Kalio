using Kalio.Core.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

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

            var _userRepository = context.HttpContext.RequestServices.GetService<IUserRepository>();

            var userId = user.Claims.FirstOrDefault(c => c.Type == "Email").Value;
            var result = _userRepository.GetPermissionsByUserIdAsync(userId).Result;
            var claims = result.Response;
            if (!claims.Contains(_claim))
            {
                context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Forbidden);
                return;
            }
        }
    }
}
