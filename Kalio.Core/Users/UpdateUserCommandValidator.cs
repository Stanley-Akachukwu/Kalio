
using FluentValidation;
using FluentValidation.Results;
using Kalio.Domain.Users;
using Kalio.Entities;
using Microsoft.AspNetCore.Identity;

namespace Kalio.Core.Users
{
    public partial class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {

        private readonly KalioIdentityDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        public UpdateUserCommandValidator(KalioIdentityDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;

            RuleFor(p => p.Email).NotEmpty().WithMessage("Email Name required.");
            RuleFor(p => p.Id).NotEmpty().WithMessage("User ID required.");
            RuleFor(p => p.UpdatedByUserId).NotEmpty().WithMessage("UpdatedByUserId required.");
            RuleFor(p => p.UserName).NotEmpty().WithMessage("UserName Name required.");


            RuleFor(p => p).Custom((data, context) =>
            {
                var user = _userManager.FindByIdAsync(data.Id).Result;
                if (user == null)
                    context.AddFailure(
                    new ValidationFailure(nameof(data.Email),
                    "User does not exist", data.Email));
            });
        }
    }
}
