
using Kalio.Common;
using MediatR;

namespace Kalio.Domain.Users
{
    public class UpdateUserCommand : IRequest<CommandResult<UserViewModel>>
    {
        public string Id { get; set; }
        public string UpdatedByUserId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
    }
}
