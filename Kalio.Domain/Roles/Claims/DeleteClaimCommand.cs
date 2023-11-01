using Kalio.Common;
using MediatR;

namespace Kalio.Domain.Roles.Claims
{
    
    public partial class DeleteClaimCommand :  IRequest<CommandResult<string>>
    {
        public int Id { get; set; }
    }
}
