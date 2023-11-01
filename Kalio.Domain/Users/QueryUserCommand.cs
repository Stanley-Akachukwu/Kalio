
using Kalio.Common;
using Kalio.Domain.Roles;
using MediatR;

namespace Kalio.Domain.Users
{
    public class QueryUserCommand : IRequest<CommandResult<IQueryable<UserViewModel>>>
    {
    }
}
