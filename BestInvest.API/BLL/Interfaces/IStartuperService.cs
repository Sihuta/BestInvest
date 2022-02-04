using BestInvest.API.BLL.DTO;
using System.Security.Claims;

namespace BestInvest.API.BLL.Interfaces
{
    public interface IStartuperService
    {
        Task<AccountDTO> GetFullInfoAsync(ClaimsPrincipal user);
        Task<bool> UpdateAsync(ClaimsPrincipal user, AccountDTO account);
    }
}
