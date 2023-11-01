using FluentValidation;
using FluentValidation.Results;
using Kalio.Domain.Users;
using Kalio.Entities;
using Microsoft.AspNetCore.Identity;

namespace Kalio.Core.Users
{
    public partial class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {

        private readonly KalioIdentityDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        public CreateUserCommandValidator(KalioIdentityDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;

            RuleFor(p => p.Email).NotEmpty().WithMessage("Email Name required.");
            RuleFor(p => p.ConfirmEmail).NotEmpty().WithMessage("ConfirmEmail Name required.");
            RuleFor(p => p.Password).NotEmpty().WithMessage("Password Name required.");
            RuleFor(p => p.ConfirmPassword).NotEmpty().WithMessage("ConfirmPassword Name required.");


            RuleFor(p => p).Custom((data, context) =>
            {
                if (data.Email != data.ConfirmEmail)
                    context.AddFailure(
                    new ValidationFailure(nameof(data.Email),
                    "Confirm Email doesn't match the Email", data.Email));

                if (data.Password != data.ConfirmPassword)
                    context.AddFailure(
                    new ValidationFailure(nameof(data.Password),
                    "Confirm password doesn't match the password", data.Password));

                var user = _userManager.FindByEmailAsync(data.Email).Result;
                if (user != null)
                    context.AddFailure(
                    new ValidationFailure(nameof(data.Email),
                    "Email address already exist", data.Email));


                var passwordValidator = new PasswordValidator<IdentityUser>();
                var result = passwordValidator.ValidateAsync(_userManager, null, data.Password).Result;

                List<IdentityError> errorList = result.Errors.ToList();
                var errors = string.Join(", ", errorList.Select(e => e.Description));

                if (!result.Succeeded)
                    context.AddFailure(
                    new ValidationFailure(nameof(data.Password),
                   errors, data.Password));
            });
        }
    }
}
 