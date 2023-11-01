using AutoMapper;
using Kalio.Common;
using Kalio.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Kalio.Domain.Users;
using System.Data.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Kalio.Domain.Roles;
using Microsoft.EntityFrameworkCore;

namespace Kalio.Core.Users
{
    
    public class UserCommandHandler : IRequestHandler<QueryUserCommand, CommandResult<IQueryable<UserViewModel>>>,
   IRequestHandler<CreateUserCommand, CommandResult<UserViewModel>>,
    IRequestHandler<UpdateUserCommand, CommandResult<UserViewModel>>,
    IRequestHandler<DeleteUserCommand, CommandResult<string>>,
     IRequestHandler<ForgetPasswordCommand, CommandResult<string>>,
     IRequestHandler<ResetPasswordCommand, CommandResult<string>>,
     IRequestHandler<AuthenticateUserCommand, CommandResult<AuthenticatedResult>>,
     IRequestHandler<GetUserClaimsCommand, CommandResult<List<string>>>,
    IRequestHandler<UpdateClaimsForUserCommand, CommandResult<IQueryable<Claim>>>,
    IRequestHandler<AddUserInRoleCommand, CommandResult<RoleViewModel>>

    {

        private readonly KalioIdentityDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private IConfiguration _configuration;

        public UserCommandHandler(KalioIdentityDbContext dbContext, IMapper mapper, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, IConfiguration configuration)
        {

            _dbContext = dbContext;
            _mapper = mapper;
            _roleManager = roleManager;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<CommandResult<IQueryable<UserViewModel>>> Handle(QueryUserCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<IQueryable<UserViewModel>>();
            var userViewModels= new List<UserViewModel>();

            var users =  _userManager.Users.ToList();
            foreach (var u in users)
            {
                var userRoles = await _userManager.GetRolesAsync(u);
                userViewModels.Add(new UserViewModel {
                    Email = u.Email,
                    Id = u.Id.ToString(),
                    UserName = u.UserName,
                    Roles = userRoles.ToList()
                });
            }
            rsp.Response = userViewModels.AsQueryable();
            return rsp;
        }

        public async Task<CommandResult<UserViewModel>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<UserViewModel>();
            var identityUser = new IdentityUser
            {
                Email = request.Email,
                UserName = request.Email.Substring(0, request.Email.IndexOf("@"))
            };
            var result = await _userManager.CreateAsync(identityUser, request.Password);

            var u = await _userManager.FindByEmailAsync(request.Email);
            var user = new UserViewModel
            {
                Email = u.Email,
                Id = u.Id.ToString(),
                UserName = u.UserName,
            };

            //if (result.Succeeded)
            //{
            //    var confirmEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(identityUser);

            //    var encodedEmailToken = Encoding.UTF8.GetBytes(confirmEmailToken);
            //    var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

            //    string url = $"{_configuration["AppUrl"]}/api/users/confirmemail?userid={identityUser.Id}&token={validEmailToken}";

            //    await _mailService.SendEmailAsync(identityUser.Email, "Confirm your email", $"<h1>Welcome to Auth Demo</h1>" +
            //        $"<p>Please confirm your email by <a href='{url}'>Clicking here</a></p>");
            //}

            rsp.Response = user;
            return rsp;
        }

        public async Task<CommandResult<UserViewModel>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<UserViewModel>();
            var user = await _userManager.FindByIdAsync(request.Id);
            
            user.Email = request.Email;
            user.UserName = request.UserName;
            var result = await _userManager.UpdateAsync(user);


            var serViewModel = new UserViewModel
            {
                Email = user.Email,
                Id = user.Id.ToString(),
                UserName = user.UserName,
            };

            rsp.Response = serViewModel;
            return rsp;
        }

        public async Task<CommandResult<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<string>();

            try
            {
                var user = await _userManager.FindByIdAsync(request.Id);
                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    rsp.Message = $"user with Id = {request.Id} has been removed successfully";
                    return rsp;
                }
            }
            catch (Exception ex)
            {
                rsp.Message = $"User with ID {request.Id} could not be deleted due to error - " + ex.StackTrace;
                rsp.StatusCode = StatusCodes.Status500InternalServerError;
            }
            return rsp;
        }

        public async Task<CommandResult<string>> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<string>();

            var user = await _userManager.FindByEmailAsync(request.Email);
            
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = Encoding.UTF8.GetBytes(token);
            var validToken = WebEncoders.Base64UrlEncode(encodedToken);

            //string url = $"{_configuration["AppUrl"]}/ResetPassword?email={email}&token={validToken}";

            //await _mailService.SendEmailAsync(email, "Reset Password", "<h1>Follow the instructions to reset your password</h1>" +
            //    $"<p>To reset your password <a href='{url}'>Click here</a></p>");

            string url = $"{_configuration["AppUrl"]}/ResetPassword?email={request.Email}&token={validToken}";

            rsp.Message = "<h1>Follow the instructions to reset your password</h1>" +
                $"<p>To reset your password <a href='{url}'>Click here</a></p>";
            
            return rsp;
        }
        public async Task<CommandResult<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<string>();

            var user = await _userManager.FindByEmailAsync(request.Email);

            var decodedToken = WebEncoders.Base64UrlDecode(request.ResetPasswordToken);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManager.ResetPasswordAsync(user, normalToken, request.NewPassword);
            if(result.Succeeded)
                rsp.Message = $"<p>Password has been reset successfully!</p>";

            else
                rsp.Message = $"<p>Password reset was not successfully!</p>";


            return rsp;
        }
        public async Task<CommandResult<AuthenticatedResult>> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<AuthenticatedResult>();
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user==null)
            {
                rsp.Message = "Invalid Email";
                rsp.StatusCode = StatusCodes.Status400BadRequest;
                return rsp;
            }
            var result = await _userManager.CheckPasswordAsync(user, request.Password);
            if (result==false)
            {
                rsp.Message = "Invalid Password.";
                rsp.StatusCode = StatusCodes.Status400BadRequest;
                return rsp;
            }


            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:SecreteKey"]);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature);
            var expires = DateTime.UtcNow.AddMinutes(10);

            var userClaims = new List<Claim>
                {
                    new Claim("Email", request.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                     new Claim(ClaimTypes.NameIdentifier, user.Id),
                };

            var subject = new ClaimsIdentity(userClaims);


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = subject,
                Expires = expires,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);


            if (!string.IsNullOrEmpty(jwtToken))
            {
                var authenticatedUser = new AuthenticatedResult { Token = jwtToken };
                rsp.Response = authenticatedUser;
                rsp.Message = "OK";
            }
            else { rsp.Message = "Failed."; rsp.StatusCode = 400; }

            return await Task.FromResult(rsp);
             
        }
        public async Task<CommandResult<RoleViewModel>> Handle(AddUserInRoleCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<RoleViewModel>();
            var users = new List<UsersInRole>();
            foreach (var email in request.UserEmails)
            {
              var identityUser = await _userManager.FindByEmailAsync(email);
               
                if (!(await _userManager.IsInRoleAsync(identityUser, request?.RoleName))) 
                {
                    await _userManager.AddToRoleAsync(identityUser, request?.RoleName);
                }
            }
            var usersInRole = await _userManager.GetUsersInRoleAsync(request?.RoleName);
            foreach (var user in usersInRole)
                users.Add(new UsersInRole { UserId = user?.Id, Email=user?.Email });  

            var roleViewModel = new RoleViewModel()
            {
                RoleName = request?.RoleName,
                RoleId = request?.RoleId,
                UsersInRole = users,
            };
            rsp.Message = $"Role was found";
            rsp.Response = roleViewModel;
            return rsp;
        }
        public async Task<CommandResult<IQueryable<Claim>>> Handle(UpdateClaimsForUserCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<IQueryable<Claim>>();

            var claims = new List<Claim>();
             foreach(var claim in request.Claims)
                claims.Add(new Claim(type: claim, value: claim));


            var user = await _userManager.FindByIdAsync(request.UserId);

            var existingClaims = await _userManager.GetClaimsAsync(user);
            var result = await _userManager.RemoveClaimsAsync(user, existingClaims);

            result = await _userManager.AddClaimsAsync(user, claims);
         
            var  updatedClaims = await _userManager.GetClaimsAsync(user);
           
            rsp.Response = updatedClaims.AsQueryable<Claim>();
            return rsp;

        }

        public async Task<CommandResult<List<string>>> Handle(GetUserClaimsCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<List<string>>();
            var userClaims = new List<string>();
            var user = await _userManager.FindByEmailAsync(request.UserId);
            var claims = await _userManager.GetClaimsAsync(user);
            foreach (var claim in claims)
                userClaims.Add(claim.Value);

            rsp.Response = userClaims;
            return rsp;
        }

        //public async Task<CommandResult<List<string>>> Handle(GetUserClaimsCommand request, CancellationToken cancellationToken)
        //{
        //    var rsp = new CommandResult<List<string>>();
        //    var claims = _dbContext.RoleClaims.ToList().Select(c=>c.ClaimValue).ToList();
        //    rsp.Response = claims;
        //    return rsp;
        //}
    }
}
