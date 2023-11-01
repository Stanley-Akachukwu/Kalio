using AutoMapper;
using Kalio.Common;
using Kalio.Domain.Roles.Claims;
using Kalio.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Kalio.Domain.Users;

namespace Kalio.Core.Roles.Claims
{

    public class ClaimCommandHandler : IRequestHandler<QueryClaimCommand, CommandResult<IQueryable<ClaimViewModel>>>,
 IRequestHandler<CreateClaimCommand, CommandResult<ClaimViewModel>>,
  IRequestHandler<UpdateClaimCommand, CommandResult<ClaimViewModel>>,
  IRequestHandler<DeleteClaimCommand, CommandResult<string>>
        
    {
        private readonly KalioIdentityDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        public ClaimCommandHandler(KalioIdentityDbContext dbContext, IMapper mapper, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {

            _dbContext = dbContext;
            _mapper = mapper;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<CommandResult<IQueryable<ClaimViewModel>>> Handle(QueryClaimCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<IQueryable<ClaimViewModel>>();
            var claimViewModels = new List<ClaimViewModel>();

         var roleClaims = await  _dbContext.RoleClaims.ToListAsync();
            Parallel.ForEach(roleClaims, claim =>
                    claimViewModels.Add(new ClaimViewModel()
                    {
                        Id = claim.Id,
                        Name = claim.ClaimValue,
                        ClaimType = claim.ClaimType,
                        RoleId = claim.RoleId,
                    })
                );
             
            rsp.Response = claimViewModels.AsQueryable();
            return rsp;
        }
        public async Task<CommandResult<ClaimViewModel>> Handle(CreateClaimCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<ClaimViewModel>();

            
                var result = await _dbContext.RoleClaims.AddAsync(new IdentityRoleClaim<string>
                {
                    ClaimType = request.ClaimName,
                    ClaimValue = request.ClaimName,
                    RoleId = request.RoleId
                });
                _dbContext.SaveChanges();

                rsp.Response = new ClaimViewModel
                {
                    ClaimType = result.Entity.ClaimType,
                    Id = result.Entity.Id,
                    Name = result.Entity.ClaimValue,
                    RoleId = result.Entity.RoleId
                };
           
            
            rsp.Message = "Claim was created.";
            return rsp;
        }
        public async Task<CommandResult<ClaimViewModel>> Handle(UpdateClaimCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<ClaimViewModel>();

            var claim = await _dbContext.RoleClaims.FirstOrDefaultAsync(c => c.Id == request.Id);
            claim.RoleId = request.RoleId;
            claim.ClaimValue = request.ClaimName;
            claim.ClaimType = request.ClaimType;    

            var result =  _dbContext.RoleClaims.Update(claim);
            _dbContext.SaveChanges();


            rsp.Response = new ClaimViewModel
            {
                ClaimType = result.Entity.ClaimType,
                Id = result.Entity.Id,
                Name = result.Entity.ClaimValue,
                RoleId = result.Entity.RoleId
            };
            rsp.Message = "Claim was updated.";
            return rsp;
        }

        public async Task<CommandResult<string>> Handle(DeleteClaimCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<string>();
            var claim = await _dbContext.RoleClaims.FirstOrDefaultAsync(c => c.Id == request.Id);
            _dbContext.RoleClaims.Remove(claim);
            _dbContext.SaveChanges(true);
            rsp.Message = $"Claim with Id = {request.Id} has been removed successfully";
            return rsp;
        }

       

       
    }
}
