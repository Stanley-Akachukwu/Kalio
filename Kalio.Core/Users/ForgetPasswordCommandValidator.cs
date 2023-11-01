using FluentValidation;
using FluentValidation.Results;
using Kalio.Domain.Users;
using Kalio.Entities;
using Microsoft.AspNetCore.Identity;

namespace Kalio.Core.Users
{
    public partial class ForgetPasswordCommandValidator : AbstractValidator<ForgetPasswordCommand>
    {

        private readonly KalioIdentityDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        public ForgetPasswordCommandValidator(KalioIdentityDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;

            RuleFor(p => p.Email).NotEmpty().WithMessage("Email Name required.");

            RuleFor(p => p).Custom((data, context) =>
            {
                var user = _userManager.FindByEmailAsync(data.Email).Result;
                if (user == null)
                    context.AddFailure(
                    new ValidationFailure(nameof(data.Email),
                    "User does not exist", data.Email));
            });
        }
    }
}
