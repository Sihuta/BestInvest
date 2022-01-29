using BestInvest.API.BLL.DTO;

namespace BestInvest.API.BLL.Interfaces
{
    public interface IStartuperService
    {
        Task<AccountDTO> GetFullInfo();
    }
}
