using Kalio.Common;
using MediatR;

namespace Kalio.Domain.Users
{
    public class ForgetPasswordCommand : IRequest<CommandResult<string>>
    {
        public string Email { get; set; }
    }
}
