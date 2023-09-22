using Kalio.Common;
using Kalio.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalio.Core.Services.Users
{
    public interface IUserRepository
    {
        Task<CommandResult<bool>> RegisterUserAsync(RegisterViewModel model);

        Task<CommandResult<AuthenticatedResult>> AuthenticateUserAsync(LoginViewModel model);
        Task<CommandResult<List<string>>> GetPermissionsByUserIdAsync(string userId);

        //Task<UserManagerResponse> ConfirmEmailAsync(string userId, string token);

        //Task<UserManagerResponse> ForgetPasswordAsync(string email);

        //Task<UserManagerResponse> ResetPasswordAsync(ResetPasswordViewModel model);
        //List<IdentityUser> GetUsersAsync();
        //Task<EditUserViewModel> GetUserByIdAsync(string id);
        //Task<EditUserViewModel> UpdateUserAsync(EditUserViewModel model);
        //Task<UserManagerResponse> DeleteUserAsync(string id);

    }
}
