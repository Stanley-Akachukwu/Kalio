using Kalio.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalio.Domain.Roles.Claims
{
    public class QueryClaimCommand : IRequest<CommandResult<IQueryable<ClaimViewModel>>>
    {
    }
}
