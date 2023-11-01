using FluentValidation;
using FluentValidation.Results;
using Kalio.Domain.Roles;
using Kalio.Entities;
using Microsoft.AspNetCore.Identity;

namespace Kalio.Core.Roles
{
    public partial class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
    {
        private readonly KalioDbContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        public  UpdateRoleCommandValidator(KalioDbContext dbContext, RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;

            RuleFor(p => p.RoleName).NotEmpty().WithMessage("Role Name required.");
            RuleFor(p => p.Id).NotEmpty().WithMessage("Role ID is required.");

            RuleFor(p => p).Custom((data, context) =>
            {
                var role = _roleManager.FindByIdAsync(data.Id).Result;
                if (role == null)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.Id),
                    "Role was not found", data.Id));
                }
            });
        }
    }
}
