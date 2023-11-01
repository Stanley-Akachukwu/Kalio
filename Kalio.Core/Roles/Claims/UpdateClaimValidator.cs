using FluentValidation;
using FluentValidation.Results;
using Kalio.Domain.Roles.Claims;
using Kalio.Entities;
using Microsoft.AspNetCore.Identity;


namespace Kalio.Core.Roles.Claims
{
    public class UpdateClaimValidator : AbstractValidator<UpdateClaimCommand>
    {
        private readonly KalioIdentityDbContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UpdateClaimValidator(KalioIdentityDbContext dbContext, RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;

            RuleFor(p => p.ClaimType).NotEmpty().WithMessage("Claim Type required.");
            RuleFor(p => p.ClaimName).NotEmpty().WithMessage("Claim Name required.");
            RuleFor(p => p.RoleId).NotEmpty().WithMessage("Role ID required.");

            RuleFor(p => p).Custom((data, context) =>
            {
                var role = _roleManager.FindByNameAsync(data.ClaimName).Result;

                if (role == null)

                    context.AddFailure(
                    new ValidationFailure(nameof(data.ClaimName),
                    "Claim name does not exist", data.ClaimName));
            });
        }
    }

}
