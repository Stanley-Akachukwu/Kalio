
using FluentValidation;
using Kalio.Entities;
using Microsoft.AspNetCore.Identity;

namespace Kalio.Core.Users
{
   
    public partial class AuthenticateUserCommandValidator : AbstractValidator<AuthenticateUserCommand>
    {

        private readonly KalioIdentityDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        public AuthenticateUserCommandValidator(KalioIdentityDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;

            RuleFor(p => p.Email).NotEmpty().WithMessage("Email Name required.");
            RuleFor(p => p.Password).NotEmpty().WithMessage("Password Name required.");
           
        }
    }
}
