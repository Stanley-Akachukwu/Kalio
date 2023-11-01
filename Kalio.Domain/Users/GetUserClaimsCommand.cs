using Kalio.Common;
using MediatR;
using System.Security.Claims;

namespace Kalio.Domain.Users
{
    public class GetUserClaimsCommand : IRequest<CommandResult<List<string>>>
    {
        public string UserId { get; set; }
    }
}
