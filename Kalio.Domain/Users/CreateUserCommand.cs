using Kalio.Common;
using MediatR;

namespace Kalio.Domain.Users
{
    public class CreateUserCommand : IRequest<CommandResult<UserViewModel>>
    {
        public string Email { get; set; }
        public string ConfirmEmail { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
