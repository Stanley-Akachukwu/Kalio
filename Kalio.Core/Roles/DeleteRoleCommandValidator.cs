using FluentValidation;
using FluentValidation.Results;
using Kalio.Domain.Roles;
using Kalio.Entities;
using Microsoft.AspNetCore.Identity;

namespace Kalio.Core.Roles
{
   
    public partial class DeleteRoleCommandValidator : AbstractValidator<DeleteRoleCommand>
    {

        private readonly RoleManager<IdentityRole> _roleManager;
        public DeleteRoleCommandValidator(KalioDbContext dbContext, RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
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
