using FluentValidation;
using FluentValidation.Results;
using Kalio.Domain.Users;
using Kalio.Entities;
using Microsoft.AspNetCore.Identity;

namespace Kalio.Core.Users
{
    
    public partial class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {

        private readonly KalioIdentityDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        public DeleteUserCommandValidator(KalioIdentityDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;

            RuleFor(p => p.Id).NotEmpty().WithMessage("User ID is required.");

            RuleFor(p => p).Custom((data, context) =>
            {
                var user = _userManager.FindByIdAsync(data.Id).Result;
                if (user == null)
                    context.AddFailure(
                    new ValidationFailure(nameof(data.Id),
                    "User does not exist", data.Id));
            });
        }
    }
}
