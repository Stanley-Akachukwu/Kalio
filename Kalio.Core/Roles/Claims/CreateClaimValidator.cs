using Azure.Core;
using FluentValidation;
using FluentValidation.Results;
using Kalio.Domain.Roles.Claims;
using Kalio.Entities;
using Microsoft.AspNetCore.Identity;

namespace Kalio.Core.Roles.Claims
{
    
    public partial class CreateClaimValidator : AbstractValidator<CreateClaimCommand>
    {

        private readonly KalioIdentityDbContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;

        public CreateClaimValidator(KalioIdentityDbContext dbContext, RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;

            RuleFor(p => p.RoleName).NotEmpty().WithMessage("Role Name required.");
            RuleFor(p => p.ClaimName).NotEmpty().WithMessage("Claim Name required.");
            RuleFor(p => p.RoleId).NotEmpty().WithMessage("Role ID required.");

            RuleFor(p => p).Custom((data, context) =>
            {
                var role = _roleManager.FindByNameAsync(data.RoleName).Result;

                if (role == null)
                    context.AddFailure(
                    new ValidationFailure(nameof(data.RoleName),
                    "Role name does not exist", data.RoleName));

                var dbClaim = _dbContext.RoleClaims.Where(c => c.ClaimValue == data.ClaimName).FirstOrDefault();
                if (dbClaim != null)
                    context.AddFailure(
                    new ValidationFailure(nameof(data.ClaimName),
                    "Claim already exist", data.ClaimName));

            });
        }
    }
}
