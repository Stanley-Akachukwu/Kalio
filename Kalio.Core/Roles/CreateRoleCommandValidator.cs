using FluentValidation;
using FluentValidation.Results;
using Kalio.Domain.Roles;
using Kalio.Entities;
using Microsoft.AspNetCore.Identity;

namespace Kalio.Core.Roles
{
    public partial class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
    {

        private readonly KalioIdentityDbContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;

        public CreateRoleCommandValidator(KalioIdentityDbContext dbContext, RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;

            RuleFor(p => p.RoleName).NotEmpty().WithMessage("Role Name required.");

            RuleFor(p => p).Custom((data, context) =>
            {
                var role = _roleManager.FindByNameAsync(data.RoleName).Result;

                if (role != null)

                    context.AddFailure(
                    new ValidationFailure(nameof(data.RoleName),
                    "Role name already exist", data.RoleName));
            });
        }
    }

}
