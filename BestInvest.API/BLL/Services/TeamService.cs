using BestInvest.API.BLL.DTO;
using BestInvest.API.BLL.Interfaces;
using BestInvest.API.DAL.EF;
using BestInvest.API.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BestInvest.API.BLL.Services
{
    public class TeamService : ITeamService
    {
        private readonly AppDbContext dbContext;
        private readonly IdentityService identityService;
        private readonly Mapper mapper;

        public TeamService(AppDbContext appDbContext, IdentityService identityService, Mapper mapper)
        {
            dbContext = appDbContext;
            this.identityService = identityService;
            this.mapper = mapper;
        }

        public async Task<List<TeamDTO>> GetCreatedTeamsAsync(ClaimsPrincipal user)
        {
            var currentUser = await identityService.GetCurrentUserAsync(user);
            var account = await dbContext.Accounts
                .Where(a => a.Login == currentUser.UserName)
                .FirstOrDefaultAsync();

            var accountMemberships = await dbContext.TeamMembers
                .Include(tm => tm.Team)
                .Where(tm => tm.AccountId == account.Id)
                .ToListAsync();

            return accountMemberships.Select(am => new TeamDTO
            {
                Id = am.TeamId,
                Name = am.Team.Name,
                TeamMembers = mapper.Map<TeamMember, TeamMemberDTO>(
                    dbContext.TeamMembers.Where(t => t.TeamId == am.TeamId).ToList()),
            }).ToList();
        }
    }
}
