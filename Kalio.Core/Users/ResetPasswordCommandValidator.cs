using FluentValidation;
using FluentValidation.Results;
using Kalio.Domain.Users;
using Kalio.Entities;
using Microsoft.AspNetCore.Identity;

namespace Kalio.Core.Users
{
    
    public partial class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {

        private readonly KalioIdentityDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        public ResetPasswordCommandValidator(KalioIdentityDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;

            RuleFor(p => p.Email).NotEmpty().WithMessage("Email required.");
            RuleFor(p => p.NewPassword).NotEmpty().WithMessage("NewPassword required.");
            RuleFor(p => p.ConfirmPassword).NotEmpty().WithMessage("ConfirmPassword required.");
            RuleFor(p => p.ResetPasswordToken).NotEmpty().WithMessage("ResetPasswordToken required.");

            RuleFor(p => p).Custom((data, context) =>
            {
                if (data.NewPassword != data.ConfirmPassword)
                    context.AddFailure(
                    new ValidationFailure(nameof(data.NewPassword),
                    "Confirm password doesn't match the new password", data.NewPassword));

                var user = _userManager.FindByIdAsync(data.Email).Result;
                if (user == null)
                    context.AddFailure(
                    new ValidationFailure(nameof(data.Email),
                    "User does not exist", data.Email));


            var passwordValidator = new PasswordValidator<IdentityUser>();
                var result = passwordValidator.ValidateAsync(_userManager, null, data.NewPassword).Result;

                List<IdentityError> errorList = result.Errors.ToList();
                var errors = string.Join(", ", errorList.Select(e => e.Description));

                if (!result.Succeeded)
                    context.AddFailure(
                    new ValidationFailure(nameof(data.NewPassword),
                   errors, data.NewPassword));
            });
        }
    }
}
