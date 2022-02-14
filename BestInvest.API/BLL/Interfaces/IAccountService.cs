using BestInvest.API.BLL.DTO;
using System.Security.Claims;

namespace BestInvest.API.BLL.Interfaces
{
    public interface IAccountService
    {
        Task<AccountDTO> GetAsync(int id);
        Task<AccountDTO> FindByLoginAsync(string login);
        Task<bool> CreateAsync(AccountDTO accountDTO);
        Task<bool> ChangePasswordAsync(ClaimsPrincipal user, ChangePasswordDTO changePasswordDTO);
    }
}
