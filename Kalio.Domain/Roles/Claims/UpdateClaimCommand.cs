using Kalio.Common;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Kalio.Domain.Roles.Claims
{

     
    public class UpdateClaimCommand : IRequest<CommandResult<ClaimViewModel>>
    {
        public int Id { get; set; }
        public string RoleId { get; set; }
        [Display(Name = "Claim")]
        public string ClaimName { get; set; }
        public string ClaimType { get; set; }
    }
}
