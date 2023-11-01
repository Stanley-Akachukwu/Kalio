using Kalio.Common;
using MediatR;

namespace Kalio.Domain.Roles
{
    public class QueryRoleCommand : IRequest<CommandResult<IQueryable<RoleViewModel>>>
    {

    }

}
