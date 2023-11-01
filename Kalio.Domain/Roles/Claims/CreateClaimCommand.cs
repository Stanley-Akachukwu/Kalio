using Kalio.Common;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Kalio.Domain.Roles.Claims
{
    public class CreateClaimCommand : IRequest<CommandResult<ClaimViewModel>>
    {
        [Display(Name = "Role")]
        public string RoleName { get; set; }
        public string RoleId { get; set; }
        [Display(Name = "Claim")]
        public string ClaimName { get; set; }
    }
}
