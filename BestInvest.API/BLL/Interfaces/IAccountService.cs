using BestInvest.API.BLL.DTO;
using System.Security.Claims;

namespace BestInvest.API.BLL.Interfaces
{
    public interface IAccountService
    {
        Task<AccountDTO> FindByLogin(string login);
        Task<bool> CreateAsync(AccountDTO accountDTO);
        Task<bool> ChangePasswordAsync(ClaimsPrincipal user, ChangePasswordDTO changePasswordDTO);
    }
}
