
using Kalio.Common;
using MediatR;

namespace Kalio.Domain.Users
{
    public class DeleteUserCommand : DeleteCommand, IRequest<CommandResult<string>>
    {
    }
}
