using BestInvest.API.BLL.DTO;

namespace BestInvest.API.BLL.Interfaces
{
    public interface IAccountService
    {
        Task<bool> Create(AccountDTO account);
    }
}
