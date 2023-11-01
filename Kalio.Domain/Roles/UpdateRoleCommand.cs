using Kalio.Common;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Kalio.Domain.Roles
{
   
    public partial class UpdateRoleCommand :  IRequest<CommandResult<RoleViewModel>>
    {
        public string Id { get; set; }
        public string UpdatedByUserId { get; set; }
        [Display(Name = "Role")]
        public string RoleName { get; set; }
    }

}
