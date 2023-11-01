using Kalio.Common;
using Kalio.Domain.Users;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Kalio.Core.Users
{
    public class AuthenticateUserCommand : IRequest<CommandResult<AuthenticatedResult>>
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
