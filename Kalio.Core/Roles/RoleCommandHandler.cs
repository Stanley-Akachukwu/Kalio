using AutoMapper;
using Kalio.Common;
using Kalio.Entities;
using MediatR;
using Kalio.Domain.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
namespace Kalio.Core.Roles
{

    public class RoleCommandHandler :IRequestHandler<QueryRoleCommand, CommandResult<IQueryable<RoleViewModel>>>,
   IRequestHandler<CreateRoleCommand, CommandResult<RoleViewModel>>,
    IRequestHandler<UpdateRoleCommand, CommandResult<RoleViewModel>>,
    IRequestHandler<DeleteRoleCommand, CommandResult<string>>
   
    {

        private readonly KalioIdentityDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        public RoleCommandHandler(KalioIdentityDbContext dbContext, IMapper mapper, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {

            _dbContext = dbContext;
            _mapper = mapper;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<CommandResult<IQueryable<RoleViewModel>>> Handle(QueryRoleCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<IQueryable<RoleViewModel>>();
            var roleViewModels = new List<RoleViewModel>();
            var users = new List<UsersInRole>();


            var roles = await _roleManager.Roles.ToListAsync();
            foreach(var role in roles)
            {
                var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name);

                foreach (var user in usersInRole)
                    users.Add(new UsersInRole { UserId = user?.Id, Email = user?.Email });


                roleViewModels.Add( new RoleViewModel() 
                { 
                    RoleName=role.Name,
                    UsersInRole = users,
                });
            }
            rsp.Response = roleViewModels.AsQueryable();
            return rsp;
        }
        public async Task<CommandResult<RoleViewModel>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<RoleViewModel>();
            IdentityRole identityRole = new IdentityRole
            {
                Name = request.RoleName
            };
            var result = await _roleManager.CreateAsync(identityRole);

            rsp.Response = new RoleViewModel 
            {
               RoleName = identityRole.Name
            };
            rsp.Message = "Role was created.";
            return rsp;
        }
        public async Task<CommandResult<RoleViewModel>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<RoleViewModel>();
            var users = new List<UsersInRole>();

            IdentityRole identityRole = new IdentityRole
            {
                Name = request.RoleName,
                Id = request.Id
            };
            var result = await _roleManager.UpdateAsync(identityRole);
 
            var usersInRole = await _userManager.GetUsersInRoleAsync(identityRole.Name);
            foreach (var user in usersInRole)
                users.Add(new UsersInRole { UserId = user?.Id, Email = user?.Email });


            var roleViewModel = new RoleViewModel()
            {
                RoleName = identityRole.Name,
                UsersInRole = users,
            };
            rsp.Message = $"Role was found";
            rsp.Response = roleViewModel;
            return rsp;
        }

        public async Task<CommandResult<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<string>();

            var role = await _roleManager.FindByIdAsync(request.Id);
                try
                {
                    var result = await _roleManager.DeleteAsync(role);
                    if (result.Succeeded)
                    {
                        rsp.Message = $"Role with Id = {request.Id} has been removed successfully";
                        rsp.StatusCode = StatusCodes.Status404NotFound;
                        return rsp;
                    }
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
                {
                    rsp.Message = $"{role.Name} role cannot be deleted as there are users " +
                        $"in this role. If you want to delete this role, please remove the users from " +
                        $"the role and then try to delete-" + ex.StackTrace;
                    rsp.StatusCode = StatusCodes.Status404NotFound;
                }
            return rsp;
        }

       
    }
}
