using BestInvest.API.BLL.DTO;
using System.Security.Claims;

namespace BestInvest.API.BLL.Interfaces
{
    public interface IAccountService
    {
        Task<bool> CreateAsync(AccountDTO account);

        Task<bool> ChangePasswordAsync(ClaimsPrincipal user, ChangePasswordDTO changePasswordDTO);
    }
}
