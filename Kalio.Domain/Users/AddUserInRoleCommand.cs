
using Kalio.Common;
using Kalio.Domain.Roles;
using MediatR;

namespace Kalio.Domain.Users
{
    public class AddUserInRoleCommand : IRequest<CommandResult<RoleViewModel>>
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public List<string> UserEmails { get; set; }
    }
}
