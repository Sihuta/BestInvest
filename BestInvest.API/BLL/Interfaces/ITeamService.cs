using BestInvest.API.BLL.DTO;
using System.Security.Claims;

namespace BestInvest.API.BLL.Interfaces
{
    public interface ITeamService
    {
        Task<List<TeamDTO>> GetCreatedTeamsAsync(ClaimsPrincipal user);
    }
}
