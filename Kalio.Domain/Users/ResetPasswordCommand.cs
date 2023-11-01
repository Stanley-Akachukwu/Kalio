using Kalio.Common;
using MediatR;

namespace Kalio.Domain.Users
{
    public class ResetPasswordCommand : IRequest<CommandResult<string>>
    {
        public string Email { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmPassword { get; set; }
        public string ResetPasswordToken { get; set; }
    }
}
