using FluentValidation;
using FluentValidation.Results;
using Kalio.Domain.Users;
using Kalio.Entities;
using Microsoft.AspNetCore.Identity;

namespace Kalio.Core.Users
{
     
    public partial class AddUserInRoleCommandValidator : AbstractValidator<AddUserInRoleCommand>
    {

        private readonly KalioIdentityDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AddUserInRoleCommandValidator(KalioIdentityDbContext dbContext, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;


            RuleFor(p => p).Custom((data, context) =>
            {
                var result =  _roleManager.RoleExistsAsync(data.RoleName).Result;
                if (result == false)
                    context.AddFailure(
                    new ValidationFailure(nameof(data.RoleName),
                    "Specified Role was not found", data.RoleName));
            });
        }
    }
}
