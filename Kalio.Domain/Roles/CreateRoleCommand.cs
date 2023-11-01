using Kalio.Common;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Kalio.Domain.Roles
{
    public class CreateRoleCommand:IRequest<CommandResult<RoleViewModel>>
    {
        [Display(Name = "Role")]
        public string RoleName { get; set; }
    }
}
