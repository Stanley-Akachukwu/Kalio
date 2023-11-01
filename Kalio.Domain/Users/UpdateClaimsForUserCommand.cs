using Kalio.Common;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Kalio.Domain.Users
{
    public class UpdateClaimsForUserCommand : IRequest<CommandResult<IQueryable<Claim>>>
    {
        public string UserId { get; set; }
       public List<string> Claims { get; set; }

    }
}
