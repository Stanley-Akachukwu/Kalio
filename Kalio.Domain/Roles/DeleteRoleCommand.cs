using Kalio.Common;
using MediatR;

namespace Kalio.Domain.Roles
{
    public partial class DeleteRoleCommand : DeleteCommand, IRequest<CommandResult<string>>
    {

    }

}
